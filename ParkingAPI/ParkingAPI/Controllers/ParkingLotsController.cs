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
    public class ParkingLotsController : ControllerBase
    {
        private readonly ParkAPIContext _context;

        public ParkingLotsController(ParkAPIContext context)
        {
            _context = context;

        }

        // GET: api/ParkingLots
        [HttpGet]
        public IEnumerable<ParkingLots> GetParkingLots()
        {
            return _context.ParkingLots;
        }

        // GET: api/ParkingLots/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParkingLots([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parkingLots = await _context.ParkingLots.FindAsync(id);

            if (parkingLots == null)
            {
                return NotFound();
            }

            return Ok(parkingLots);
        }

        // PUT: api/ParkingLots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingLots([FromRoute] string id, [FromBody] ParkingLots parkingLots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkingLots.LotId)
            {
                return BadRequest();
            }

            _context.Entry(parkingLots).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingLotsExists(id))
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

        // POST: api/ParkingLots
        [HttpPost]
        public async Task<IActionResult> PostParkingLots([FromBody] ParkingLots parkingLots)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ParkingLots.Add(parkingLots);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParkingLotsExists(parkingLots.LotId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParkingLots", new { id = parkingLots.LotId }, parkingLots);
        }

        // DELETE: api/ParkingLots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingLots([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parkingLots = await _context.ParkingLots.FindAsync(id);
            if (parkingLots == null)
            {
                return NotFound();
            }

            _context.ParkingLots.Remove(parkingLots);
            await _context.SaveChangesAsync();

            return Ok(parkingLots);
        }

        private bool ParkingLotsExists(string id)
        {
            return _context.ParkingLots.Any(e => e.LotId == id);
        }
    }
}