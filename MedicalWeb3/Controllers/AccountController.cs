using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MedicalWeb3.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string surname, string login, string password)
        {
            ResearcherClient api = new ResearcherClient();
            try
            {
                var researcher = await api.PostResearcherAsync(new Researcher()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Surname = surname,
                    Login = login,
                    Password = password
                });
            }
            catch
            {
                return View(model: "This login is already registered");
            }
            return View("Registered");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            AccountClient api = new AccountClient();
            try
            {
                var response = await api.TokenAsync(login, password);
                var option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("token", response, option);
            }
            catch
            {
                return View(model:"Incorrect login or password");
            }

            return RedirectToAction(nameof(Index), "Participant");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //AccountClient api = new AccountClient();
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("token", "", option);

            return RedirectToAction(nameof(Index), "Home");
        }

    }
}
