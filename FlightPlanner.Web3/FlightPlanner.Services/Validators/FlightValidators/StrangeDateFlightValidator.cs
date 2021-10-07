using System;
using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.FlightValidators
{
    public class StrangeDateFlightValidator : IFlightValidator
    {
        public bool IsValidFlight(FlightRequest request)
        {
            try
            {
                return Convert.ToDateTime(request.ArrivalTime) > Convert.ToDateTime(request.DepartureTime);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
