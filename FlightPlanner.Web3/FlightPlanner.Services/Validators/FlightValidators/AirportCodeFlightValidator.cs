using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.FlightValidators
{
    public class AirportCodeFlightValidator : IFlightValidator
    {
        public bool IsValidFlight(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.To?.Airport) &&
                   !string.IsNullOrEmpty(request.From?.Airport);
        }
    }
}
