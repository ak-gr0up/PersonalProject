using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MedicalClient;
using MedicalWeb3.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalWeb3.Controllers
{

    public class ParticipantController : Controller
    {

        private List<List<double>> CountValues(ICollection<DataPoint> data)
        {
            var INF = 1000000000.0;
            var res = new List<List<double>> { Enumerable.Repeat(0.0, 5).ToList(), Enumerable.Repeat(0.0, 5).ToList(), Enumerable.Repeat(INF, 5).ToList(), Enumerable.Repeat(0.0, 5).ToList() };
            foreach (var point in data)
            {
                res[0][0] += point.Temperature;
                res[0][1] += point.HeartBeat;
                res[0][2] += point.SistalPressure;
                res[0][3] += point.DistalPressure;
                res[0][4] += point.SelfFeeling;
                res[2][0] = Math.Min(res[2][0], point.Temperature);
                res[2][1] = Math.Min(res[2][1], point.HeartBeat);
                res[2][2] = Math.Min(res[2][2], point.SistalPressure);
                res[2][3] = Math.Min(res[2][3], point.DistalPressure);
                res[2][4] = Math.Min(res[2][4], point.SelfFeeling);
                res[3][0] = Math.Max(res[3][0], point.Temperature);
                res[3][1] = Math.Max(res[3][1], point.HeartBeat);
                res[3][2] = Math.Max(res[3][2], point.SistalPressure);
                res[3][3] = Math.Max(res[3][3], point.DistalPressure);
                res[3][4] = Math.Max(res[3][4], point.SelfFeeling);
            }
            for (int i = 0; i < 5; i++)
                res[0][i] /= data.ToArray().Length;
            foreach (var point in data)
            {
                res[1][0] += Math.Pow(Math.Abs(res[0][0] - point.Temperature), 2);
                res[1][1] += Math.Pow(Math.Abs(res[0][1] - point.HeartBeat), 2);
                res[1][2] += Math.Pow(Math.Abs(res[0][2] - point.SistalPressure), 2);
                res[1][3] += Math.Pow(Math.Abs(res[0][3] - point.DistalPressure), 2);
                res[1][4] += Math.Pow(Math.Abs(res[0][4] - point.SelfFeeling), 2);
            }
            return res;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ParticipantClient api = new ParticipantClient(Request.Cookies["token"]);
                DataPointClient api2 = new DataPointClient(Request.Cookies["token"]);
                var data = await api2.GetDataPointAllAsync();
                var res = CountValues(data);

                var model = new ModelIndex();
                model.Data = await api.GetParticipantAsync();
                model.Table = res;
                model.Headings = new List<string> { "Среднее", "Дисперсия", "Минимум", "Максимум" };
                model.Hidden = (bool)(data.ToArray().Length == 0);

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
            try {
                DataPointClient api = new DataPointClient(Request.Cookies["token"]);
                var data = await api.GetDataPointAsync(guid);
                var res = CountValues(data);
                var model = new ModelDataPoints();
                model.Id = id;
                model.Data = data;
                model.Table = res;
                model.Headings = new List<string> { "Среднее", "Дисперсия", "Минимум", "Максимум" };
                model.Hidden = (bool)(data.ToArray().Length == 0);

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