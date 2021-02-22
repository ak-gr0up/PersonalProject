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
    public class DoctorController : ControllerBase
    {
        private readonly MedicalWebServiceContext _context;

        public DoctorController(MedicalWebServiceContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/patients")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson(Guid id)
        {
            return await _context.Person.Where(p => p.DoctorId == id).ToListAsync();
        }


        // GET: api/Doctor/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Doctor>> GetDoctor(Guid id)
        
        {
            var Doctor = await _context.Doctor.FindAsync(id);

            if (Doctor == null)
            {
                return NotFound();
            }

            return Doctor;
        }


        // PUT: api/Doctor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(Guid id, Doctor Doctor)
        {
            if (id != Doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(Doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor Doctor)
        {
            _context.Doctor.Add(Doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = Doctor.Id }, Doctor);
        }

        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(Guid id)
        {
            var Doctor = await _context.Doctor.FindAsync(id);
            if (Doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(Doctor);
            await _context.SaveChangesAsync();

            return Doctor;
        }

        private bool DoctorExists(Guid id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
