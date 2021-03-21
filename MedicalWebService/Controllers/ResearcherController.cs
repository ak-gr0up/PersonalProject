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
using Microsoft.Extensions.Logging;

namespace MedicalWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearcherController : ControllerBase
    {
        private readonly MedicalWebServiceContext _context;
        private readonly ILogger<ResearcherController> _logger;

        public ResearcherController(MedicalWebServiceContext context, ILogger<ResearcherController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Researcher/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Participant")]
        public async Task<ActionResult<Researcher>> GetResearcher()
        
        {
            try
            {
                var str_id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
                var id = Guid.Parse(str_id);
                var Researcher = await _context.Researcher.FindAsync(id);

                if (Researcher == null)
                {
                    return NotFound();
                }

                return Researcher;
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }


        // PUT: api/Researcher/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize (Roles = "Researcher")]
        public async Task<IActionResult> PutResearcher(Researcher Researcher)
        {
            try
            {
                var str_id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
                var id = Guid.Parse(str_id);

                if (id != Researcher.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Researcher).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearcherExists(id))
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
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

        // POST: api/Researcher
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Researcher>> PostResearcher(Researcher Researcher)
        {
            try
            {
                _context.Researcher.Add(Researcher);
                await _context.SaveChangesAsync();

                return Ok(Researcher);
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

        // DELETE: api/Researcher/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<Researcher>> DeleteResearcher()
        {
            try
            {
                var str_id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
                var id = Guid.Parse(str_id);

                var Researcher = await _context.Researcher.FindAsync(id);
                if (Researcher == null)
                {
                    return NotFound();
                }

                _context.Researcher.Remove(Researcher);
                await _context.SaveChangesAsync();

                return Researcher;
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }

        private bool ResearcherExists(Guid id)
        {
            try
            {
                return _context.Researcher.Any(e => e.Id == id);
            }
            catch (Exception x)
            {
                _logger.LogError(x.ToString());
                throw;
            }
        }
    }
}
