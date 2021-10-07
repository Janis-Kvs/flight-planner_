using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.FlightValidators
{
    public class AirportCityFlightValidator : IFlightValidator
    {
        public bool IsValidFlight(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.To?.City) &&
                   !string.IsNullOrEmpty(request.From?.City);
        }
    }
}
