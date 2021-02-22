﻿using System;
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
    public class ResearcherController : ControllerBase
    {
        private readonly MedicalWebServiceContext _context;

        public ResearcherController(MedicalWebServiceContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/patients")]
        [Authorize(Roles = "Researcher")]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipant(Guid id)
        {
            return await _context.Participant.Where(p => p.ResearcherId == id).ToListAsync();
        }


        // GET: api/Researcher/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Participant")]
        public async Task<ActionResult<Researcher>> GetResearcher(Guid id)
        
        {
            var Researcher = await _context.Researcher.FindAsync(id);

            if (Researcher == null)
            {
                return NotFound();
            }

            return Researcher;
        }


        // PUT: api/Researcher/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResearcher(Guid id, Researcher Researcher)
        {
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

        // POST: api/Researcher
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Researcher>> PostResearcher(Researcher Researcher)
        {
            _context.Researcher.Add(Researcher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResearcher", new { id = Researcher.Id }, Researcher);
        }

        // DELETE: api/Researcher/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Researcher>> DeleteResearcher(Guid id)
        {
            var Researcher = await _context.Researcher.FindAsync(id);
            if (Researcher == null)
            {
                return NotFound();
            }

            _context.Researcher.Remove(Researcher);
            await _context.SaveChangesAsync();

            return Researcher;
        }

        private bool ResearcherExists(Guid id)
        {
            return _context.Researcher.Any(e => e.Id == id);
        }
    }
}