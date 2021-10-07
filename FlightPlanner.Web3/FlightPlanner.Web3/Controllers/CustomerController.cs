using AutoMapper;
using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Dto.Responses;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Web2.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<ISearchFlightValidator> _searchFlightValidators;
        public CustomerController(IAirportService airportService, IMapper mapper, IFlightService flightService, IEnumerable<ISearchFlightValidator> searchFlightValidators)
        {
            _airportService = airportService;
            _mapper = mapper;
            _flightService = flightService;
            _searchFlightValidators = searchFlightValidators;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            Airport[] searchedAirport = _airportService.FindAirportByPhrase(search);
            AirportResponse[] searchedAirportResponse = _mapper.Map<AirportResponse[]>(searchedAirport);

            return Ok(searchedAirportResponse);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightRequest searchFlightRequest)
        {
            if (!_searchFlightValidators.All(s => s.IsValidSearchFlight(searchFlightRequest)))
                return BadRequest();

            Flight[] flights = _flightService.SearchFlights(searchFlightRequest);
            FlightResponse[] flightResponse = _mapper.Map<FlightResponse[]>(flights);
            SearchFlightResponse searchResults = new SearchFlightResponse(flightResponse);

            return Ok(searchResults);
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
    }
}
