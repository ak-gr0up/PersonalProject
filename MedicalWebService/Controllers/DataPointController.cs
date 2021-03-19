using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalWebService.Data;
using MedicalWebService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataPointController : ControllerBase
    {
        private readonly MedicalWebServiceContext _context;
        private readonly ILogger<DataPointController> _logger;

        public DataPointController(MedicalWebServiceContext context, ILogger<DataPointController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<IEnumerable<DataPoint>>> GetDataPoint(Guid id)
        {
            try
            {
                return await _context.DataPoint.Where(p => p.ParticipantId == id).ToListAsync();
            }
            catch(Exception x)
            {
                _logger.LogInformation(x.ToString());
                throw;
            }
        }

        [HttpGet("/all")]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<IEnumerable<DataPoint>>> GetDataPointAll()
        {
            try {
                var id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
                var guid = Guid.Parse(id);
                return await _context.DataPoint.Where(p => p.ResearcherId == guid).ToListAsync();
            }
            catch (Exception x)
            {
                _logger.LogInformation(x.ToString());
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Participant")]
        public async Task<ActionResult<DataPoint>> PostDataPoint(DataPoint data)
        {
            try
            {
                _context.DataPoint.Add(data);
                data.Id = Guid.NewGuid();
                Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value, out Guid participant_id);
                data.ParticipantId = participant_id;
                data.ResearcherId = _context.Participant.FirstOrDefault(p => p.Id == participant_id).ResearcherId;
                await _context.SaveChangesAsync();

                return data;
            }
            catch(Exception x)
            {
            _logger.LogInformation(x.ToString());
            throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<DataPoint>> DeleteParticipant(Guid id)
        {
            try
            {
                var data = await _context.DataPoint.FindAsync(id);
                if (data == null)
                {
                    return NotFound();
                }

                _context.DataPoint.Remove(data);
                await _context.SaveChangesAsync();

                return data;
            }
            catch (Exception x)
            {
                _logger.LogInformation(x.ToString());
                throw;
            }
        }

    }
}
