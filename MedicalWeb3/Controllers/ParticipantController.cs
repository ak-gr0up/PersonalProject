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
            try
            {
                ParticipantClient api = new ParticipantClient(Request.Cookies["token"]);
                var model = await api.GetParticipantAsync();
                return View(model);
            }
            catch
            {
                return View("need_to_login");
            }
        }

        [HttpGet]
        public IActionResult RegisterParticipant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterParticipant(string name, string surname, string gender, string login)
        {
            try
            {
                ParticipantClient api = new ParticipantClient(Request.Cookies["token"]);
                var Participant = await api.PostParticipantAsync(new Participant()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Surname = surname,
                    Gender = Enum.Parse<Gender>(gender),
                    Login = login,
                    BirthDate = DateTime.Now.AddYears(-14),

                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("need_to_login");
            }
        }

    }
}
