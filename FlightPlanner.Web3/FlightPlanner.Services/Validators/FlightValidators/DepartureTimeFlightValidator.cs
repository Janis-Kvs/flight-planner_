﻿using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.FlightValidators
{
    public class DepartureTimeFlightValidator :IFlightValidator
    {
        public bool IsValidFlight(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureTime);
        }
    }
}
