using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalClient;
using Microsoft.AspNetCore.Mvc;

namespace MedicalWeb3.Controllers
{
    public class ParticipantController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ParticipantClient api = new ParticipantClient();
            var model = await api.GetParticipantAsync(); 
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string name, string surname, string gender, string role, string login)
        {
            ParticipantClient api = new ParticipantClient();
            var Participant = api.PostParticipantAsync(new Participant() {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Gender = Enum.Parse<Gender>(gender),
                Login = login,
                BirthDate = DateTime.Now.AddYears(-14),

            });

            return RedirectToAction(nameof(Index));
        }
    }
}
