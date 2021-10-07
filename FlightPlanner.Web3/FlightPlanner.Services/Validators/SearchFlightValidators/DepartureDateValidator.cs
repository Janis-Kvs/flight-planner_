﻿using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.SearchFlightValidators
{
    public class DepartureDateValidator : ISearchFlightValidator
    {
        public bool IsValidSearchFlight(SearchFlightRequest searchRequest)
        {
            return !string.IsNullOrEmpty(searchRequest.DepartureDate);
        }
    }
}
