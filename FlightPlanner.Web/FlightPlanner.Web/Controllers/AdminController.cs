using FlightPlanner.Web.Models;
using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        static object _lockObj = new object();

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
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

                if (FlightStorage.IsSameFlight(flightRequest))
                    return Conflict();

                Flight flight = FlightStorage.PutFlight(flightRequest);
                return Created("", flight);
            }
        }

        [HttpDelete("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lockObj)
            {
                FlightStorage.DeleteFlight(id);
            }

            return Ok();
        }
    }
}
