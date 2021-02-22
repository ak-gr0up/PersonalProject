using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Swagger.Api;
using MedicalCommon;
using Microsoft.AspNetCore.Mvc;

namespace MedicalWeb3.Controllers
{
    public class ParticipantController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ParticipantApi api = new ParticipantApi();
            var model = api.ParticipantGetParticipantAll(); 
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string name, string surname, string gender, string role, string login)
        {
            ParticipantApi api = new ParticipantApi();
            var Participant = api.ParticipantPostParticipant(new Participant() {
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
