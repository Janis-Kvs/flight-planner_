using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.FlightValidators
{
    public class CountryFlightValidator : IFlightValidator
    {
        public bool IsValidFlight(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.To?.Country) &&
                   !string.IsNullOrEmpty(request.From?.Country);
        }
    }
}
