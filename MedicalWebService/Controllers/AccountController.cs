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

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MedicalWebServiceContext _context;

        public AccountController(MedicalWebServiceContext context)
        {
            _context = context;
        }

        [HttpPost("/tokenResearcher")]
        public ActionResult<string> Token(string username, string password)
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
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetResearcherIdentity(string username, string password)
        {
            Researcher Researcher = _context.Researcher.FirstOrDefault(p => p.Login == username && p.Password == password);
            if (Researcher != null) {
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

        [HttpPost("/tokenParticipant")]
        public ActionResult<string> TokenParticipant(string username)
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
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetParticipantIdentity(string username)
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

    }
}