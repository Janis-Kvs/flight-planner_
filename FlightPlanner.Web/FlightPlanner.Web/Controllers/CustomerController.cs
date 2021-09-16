using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Web.Interface;
using FlightPlanner.Web.Models;

namespace FlightPlanner.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            Airport[] airport = FlightStorage.FindAirportByPhrase(search);
            
            return Ok(airport);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightRequest searchFlightRequest)
        {
            if (!FlightStorage.IsValidSearchFlightRequest(searchFlightRequest))
                return BadRequest();

            IPageResult<Flight> searchResult = FlightStorage.SearchFlight(searchFlightRequest);

            return Ok(searchResult);
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }
    }
}
