using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalWebService.Data;
using MedicalWebService.Model;
using Microsoft.AspNetCore.Authorization;

namespace MedicalWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly MedicalWebServiceContext _context;

        public ParticipantController(MedicalWebServiceContext context)
        {
            _context = context;
        }

        // GET: api/Participant
        [HttpGet]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipant()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
            var guid = Guid.Parse(id);
            return await _context.Participant.Where(p => p.ResearcherId==guid).ToListAsync();
        }

        // GET: api/Participant/me
        [HttpGet("me")]
        [Authorize(Roles = "Participant")]
        public async Task<ActionResult<Participant>> GetParticipantMe()
        {
            Guid id = GetResearcherId();
            var Participant = await _context.Participant.FindAsync(id);

            if (Participant == null)
            {
                return NotFound();
            }

            return Participant;
        }

        private Guid GetResearcherId()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
            var guid = Guid.Parse(id);
            return guid;
        }

        // PUT: api/Participant/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "Researcher")]
        public async Task<IActionResult> PutParticipant(Guid id, Participant Participant)
        {
            if (id != Participant.Id)
            {
                return BadRequest();
            }

            _context.Entry(Participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Participant
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            _context.Participant.Add(participant);
            if (participant.Id == Guid.Empty)
                participant.Id = Guid.NewGuid();
            participant.ResearcherId = GetResearcherId();
            await _context.SaveChangesAsync();

            return participant;
        }

        // DELETE: api/Participant/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<Participant>> DeleteParticipant(Guid id)
        {
            var participant = await _context.Participant.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _context.Participant.Remove(participant);
            await _context.SaveChangesAsync();

            return participant;
        }

        [Authorize(Roles = "Researcher")]
        private bool ParticipantExists(Guid id)
        {
            return _context.Participant.Any(e => e.Id == id);
        }
    }
}
