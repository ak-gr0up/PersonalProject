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
    public class PersonController : ControllerBase
    {
        private readonly MedicalWebServiceContext _context;

        public PersonController(MedicalWebServiceContext context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;
            var guid = Guid.Parse(id);
            return await _context.Person.Where(p => p.DoctorId==guid).ToListAsync();
        }

        // GET: api/Person/me
        [HttpGet("me")]
        [Authorize(Roles = "Person")]
        public async Task<ActionResult<Person>> GetPersonMe()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id")?.Value;

            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Person
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(Guid id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
