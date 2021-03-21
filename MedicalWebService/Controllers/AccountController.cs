using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MedicalWebService.Model;
using MedicalWebService.Controllers;
using MedicalWebService.Data;
using Microsoft.Extensions.Logging;

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MedicalWebServiceContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(MedicalWebServiceContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("/tokenResearcher")]
        public ActionResult<string> Token(string username, string password)
        {
            try
            {
                var identity = GetResearcherIdentity(username, password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(encodedJwt);
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

        private ClaimsIdentity GetResearcherIdentity(string username, string password)
        {
            try
            {
                Researcher Researcher = _context.Researcher.FirstOrDefault(p => p.Login == username && p.Password == password);
                if (Researcher != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, Researcher.Name),
                    new Claim("id", Researcher.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Researcher")

                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }

                // если пользователя не найдено
                return null;
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

        [HttpPost("/tokenParticipant")]
        public ActionResult<string> TokenParticipant(string username)
        {
            try
            {
                var identity = GetParticipantIdentity(username);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username." });
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromSeconds(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(encodedJwt);
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

        private ClaimsIdentity GetParticipantIdentity(string username)
        {
            try
            {
                Participant Participant = _context.Participant.FirstOrDefault(p => p.Login == username);
                if (Participant != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, Participant.Name),
                    new Claim("id", Participant.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "Participant")

                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }

                // если пользователя не найдено
                return null;
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

    }
}