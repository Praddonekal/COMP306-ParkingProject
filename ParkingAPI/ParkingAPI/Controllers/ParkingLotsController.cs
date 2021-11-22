using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comp306Project.Models;
using AutoMapper;

namespace Comp306Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IMapper _mapper;

        public ParkingLotsController(IParkingRepository parkingRepository, IMapper mapper)
        {
            _parkingRepository = parkingRepository;
            _mapper = mapper;

        }

        // GET: api/ParkingLots
        [HttpGet]
        public ActionResult<IEnumerable<ParkingLots>> GetParkingLots()
        {
            var parkingLots = _parkingRepository.GetParkingLots();
            var parkingViewModel = _mapper.Map<IEnumerable<ParkingLots>>(parkingLots);
            return Ok(parkingViewModel);
        }

        // GET: api/ParkingLots/5
        [HttpGet("{id}")]
        public  IActionResult GetParkingLot(string id)
        {
            var parkingLots = _parkingRepository.GetParkingLots(id);

            if (parkingLots == null)
            {
                return NotFound();
            }

            return Ok(parkingLots);
        }

        // PUT: api/ParkingLots/5
        [HttpPut("{id}")]
        public IActionResult PutParkingLots (string id, ParkingLots parkingLots)
        {
            return Ok(_parkingRepository.Edit(id, parkingLots));

        }

        // POST: api/ParkingLots
        [HttpPost]
        public IActionResult PostParkingLots(ParkingLots parkingLots)
        {
            return Ok(_parkingRepository.Add(parkingLots));
        }

        // DELETE: api/ParkingLots/5
        [HttpDelete("{id}")]
        public IActionResult DeleteParkingLots(string id)
        {
            return Ok(_parkingRepository.Delete(id));
        }

    }
}