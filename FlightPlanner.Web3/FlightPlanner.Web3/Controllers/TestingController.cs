using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web2.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IDbServiceExtended _serviceExtended;
        static readonly object _lockObj = new object();

        public TestingController(IDbServiceExtended serviceExtended)
        {
            _serviceExtended = serviceExtended;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            lock (_lockObj)
            { 
                _serviceExtended.DeleteAll<Flight>();
                _serviceExtended.DeleteAll<Airport>();
            }

            return Ok();
        }
    }
}
