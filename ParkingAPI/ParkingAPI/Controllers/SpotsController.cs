using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comp306Project.Models;

namespace Comp306Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly ParkAPIContext _context;

        public SpotsController(ParkAPIContext context)
        {
            _context = context;
        }

        // GET: api/Spots
        [HttpGet]
        public IEnumerable<Spots> GetSpots()
        {
            return _context.Spots;
        }

        // GET: api/Spots/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpots([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spots = await _context.Spots.FindAsync(id);

            if (spots == null)
            {
                return NotFound();
            }

            return Ok(spots);
        }

        // PUT: api/Spots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpots([FromRoute] int id, [FromBody] Spots spots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spots.SpotId)
            {
                return BadRequest();
            }

            _context.Entry(spots).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpotsExists(id))
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

        // POST: api/Spots
        [HttpPost]
        public async Task<IActionResult> PostSpots([FromBody] Spots spots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Spots.Add(spots);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpotsExists(spots.SpotId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpots", new { id = spots.SpotId }, spots);
        }

        // DELETE: api/Spots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpots([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spots = await _context.Spots.FindAsync(id);
            if (spots == null)
            {
                return NotFound();
            }

            _context.Spots.Remove(spots);
            await _context.SaveChangesAsync();

            return Ok(spots);
        }

        private bool SpotsExists(int id)
        {
            return _context.Spots.Any(e => e.SpotId == id);
        }
    }
}