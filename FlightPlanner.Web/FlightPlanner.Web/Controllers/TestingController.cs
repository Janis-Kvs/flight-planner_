using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Models;
using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            foreach (var entity in _context.Airports)
                _context.Airports.Remove(entity);

            foreach (var entity in _context.Flights)
                _context.Flights.Remove(entity);

            _context.SaveChanges();
            
            return Ok();
        }
    }
}
