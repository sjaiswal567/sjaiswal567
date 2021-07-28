using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pensioner_detail_Microservices.Models;

namespace Pensioner_detail_Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailsController : ControllerBase
    {
        private readonly Context _context;

        public PensionerDetailsController(Context context)
        {
            _context = context;
        }

        // GET: api/PensionerDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionerDetails>>> GetPensioners()
        {
            return await _context.Pensioners.ToListAsync();
        }

        // GET: api/PensionerDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PensionerDetails>> GetPensionerDetails(string id)
        {
            var pensionerDetails = await _context.Pensioners.FindAsync(id);

            if (pensionerDetails == null)
            {
                return NotFound();
            }

            return pensionerDetails;
        }

        // PUT: api/PensionerDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensionerDetails(string id, PensionerDetails pensionerDetails)
        {
            if (id != pensionerDetails.PAN)
            {
                return BadRequest();
            }

            _context.Entry(pensionerDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensionerDetailsExists(id))
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

        // POST: api/PensionerDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PensionerDetails>> PostPensionerDetails(PensionerDetails pensionerDetails)
        {
            _context.Pensioners.Add(pensionerDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PensionerDetailsExists(pensionerDetails.PAN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPensionerDetails", new { id = pensionerDetails.PAN }, pensionerDetails);
        }

        // DELETE: api/PensionerDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensionerDetails(string id)
        {
            var pensionerDetails = await _context.Pensioners.FindAsync(id);
            if (pensionerDetails == null)
            {
                return NotFound();
            }

            _context.Pensioners.Remove(pensionerDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PensionerDetailsExists(string id)
        {
            return _context.Pensioners.Any(e => e.PAN == id);
        }
    }
}
