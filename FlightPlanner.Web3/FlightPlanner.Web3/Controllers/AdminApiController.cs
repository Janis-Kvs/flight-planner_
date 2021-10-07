using AutoMapper;
using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Dto.Responses;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Web2.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IFlightValidator> _validators;
        static readonly object _lockObj = new object();

        public AdminApiController(IFlightService flightService, IMapper mapper, IEnumerable<IFlightValidator> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight == null)
                return NotFound();

            var flightResponse = _mapper.Map<FlightResponse>(flight);

            return Ok(flightResponse);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(FlightRequest flightRequest)
        {
            var flight = _mapper.Map<Flight>(flightRequest);
            lock (_lockObj)
            {
                if (!_validators.All( i => i.IsValidFlight(flightRequest)))
                    return BadRequest();

                if (_flightService.Exists(flight))
                    return Conflict();

                _flightService.Create(flight);
                var flightResponse = _mapper.Map<FlightResponse>(flight);

                return Created("", flightResponse);
            }
        }

        [HttpDelete("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lockObj)
            {
                _flightService.DeleteById(id);
            }

            return Ok();
        }
    }
}
