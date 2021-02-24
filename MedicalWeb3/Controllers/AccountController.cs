using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalClient;
using Microsoft.AspNetCore.Mvc;

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
            var researcher = await api.PostResearcherAsync(new Researcher()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Login = login,
                Password = password
            });

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
            }
            catch
            {
                return View(model:"Incorrect login or password");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
