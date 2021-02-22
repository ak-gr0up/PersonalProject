using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Swagger.Api;
using MedicalCommon;
using Microsoft.AspNetCore.Mvc;

namespace MedicalWeb3.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            PersonApi api = new PersonApi();
            var model = api.PersonGetPersonAll(); 
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string name, string surname, string gender, string role, string login)
        {
            PersonApi api = new PersonApi();
            var person = api.PersonPostPerson(new Person() {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Gender = Enum.Parse<Gender>(gender),
                Login = login,
                BirthDate = DateTime.Now.AddYears(-14)
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
