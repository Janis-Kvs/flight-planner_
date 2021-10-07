using System.Linq;
using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Models;
using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        static object _lockObj = new object();
        private readonly FlightPlannerDbContext _context;

        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .SingleOrDefault(f => f.Id == id);
            
            if(flight == null) 
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest flightRequest)
        {
            lock (_lockObj)
            {
                if (!FlightStorage.IsValidFlight(flightRequest))
                    return BadRequest();

                if (FlightStorage.IsSameFlight(flightRequest, _context))
                    return Conflict();

                Flight flight = FlightStorage.CreateFlight(flightRequest);
                _context.Flights.Add(flight);
                _context.SaveChanges();

                return Created("", flight);
            }
        }

        [HttpDelete("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lockObj)
            {
                FlightStorage.DeleteFlight(id, _context);
            }

            return Ok();
        }
    }
}
