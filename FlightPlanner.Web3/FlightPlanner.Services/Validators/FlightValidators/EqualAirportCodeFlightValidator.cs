using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.FlightValidators
{
    public class EqualAirportCodeFlightValidator : IFlightValidator
    {
        public bool IsValidFlight(FlightRequest request)
        {
            return request?.From?.Airport?.Trim().ToUpper() != request?.To?.Airport?.Trim().ToUpper();
        }
    }
}
