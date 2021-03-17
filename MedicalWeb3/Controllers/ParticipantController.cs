using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

        [HttpGet]
        public async Task<IActionResult> ViewDataPoints(string id)
        {
            Guid.TryParse(id, out var guid);
            try
            {
                DataPointClient api = new DataPointClient(Request.Cookies["token"]);
                Tuple<ICollection<MedicalClient.DataPoint>, string> model = Tuple.Create(await api.GetDataPointAsync(guid), id);
            
                return View("ViewDataPoints", model);
            }
            catch
            {
                return View("need_to_login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadById(string id)
        {
            DataPointClient api = new DataPointClient(Request.Cookies["token"]);
            Guid.TryParse(id, out var guid);
            var res = await api.GetDataPointAsync(guid);
            var data = res.ToArray();
            StringBuilder sb = new StringBuilder();
            //add separator for header
            sb.Append("Temperature").Append(";")
                .Append("Heart Beat").Append(";")
                .Append("Distal Pressure").Append(";")
                .Append("Sistal Pressure").Append(";")
                .Append("Self Feeling").Append(";")
                .Append("Headache").Append(";")
                .Append("Dizziness").Append(";")
                .Append("Cough").Append(";")
                .Append("Rheum").Append(";")
                .Append("Weakness").Append(";")
                .Append("Nausea").Append(";")
                .Append("Participant Id").Append(";")
                .Append("Researcher Id").Append("\r\n");

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].Temperature).Append(";")
                .Append(data[i].HeartBeat).Append(";")
                .Append(data[i].DistalPressure).Append(";")
                .Append(data[i].SistalPressure).Append(";")
                .Append(data[i].SelfFeeling).Append(";")
                .Append(data[i].Headache).Append(";")
                .Append(data[i].Dizziness).Append(";")
                .Append(data[i].Cough).Append(";")
                .Append(data[i].Rheum).Append(";")
                .Append(data[i].Rheum).Append(";")
                .Append(data[i].Nausea).Append(";")
                .Append(data[i].ParticipantId).Append(";")
                .Append(data[i].ResearcherId).Append("\r\n");
            }
            return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", $"datapoints_{id}.csv");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadAll()
        {
            DataPointClient api = new DataPointClient(Request.Cookies["token"]);
            var res = await api.GetDataPointAllAsync();
            var data = res.ToArray();
            StringBuilder sb = new StringBuilder();
            //add separator for header
            sb.Append("Date").Append(";")
                .Append("Temperature").Append(";")
                .Append("Heart Beat").Append(";")
                .Append("Distal Pressure").Append(";")
                .Append("Sistal Pressure").Append(";")
                .Append("Self Feeling").Append(";")
                .Append("Headache").Append(";")
                .Append("Dizziness").Append(";")
                .Append("Cough").Append(";")
                .Append("Rheum").Append(";")
                .Append("Weakness").Append(";")
                .Append("Nausea").Append(";")
                .Append("Participant Id").Append(";")
                .Append("Researcher Id").Append("\r\n");

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].Time).Append(";")
                .Append(data[i].Temperature).Append(";")
                .Append(data[i].HeartBeat).Append(";")
                .Append(data[i].DistalPressure).Append(";")
                .Append(data[i].SistalPressure).Append(";")
                .Append(data[i].SelfFeeling).Append(";")
                .Append(data[i].Headache).Append(";")
                .Append(data[i].Dizziness).Append(";")
                .Append(data[i].Cough).Append(";")
                .Append(data[i].Rheum).Append(";")
                .Append(data[i].Rheum).Append(";")
                .Append(data[i].Nausea).Append(";")
                .Append(data[i].ParticipantId).Append(";")
                .Append(data[i].ResearcherId).Append("\r\n");
            }
            return File(Encoding.ASCII.GetBytes(sb.ToString()), "text/csv", $"datapoints_all.csv");
        }

    }
}