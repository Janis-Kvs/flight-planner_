using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.SearchFlightValidators
{
    public class AirportValidator : ISearchFlightValidator
    {
        public bool IsValidSearchFlight(SearchFlightRequest searchRequest)
        {
            return !string.IsNullOrEmpty(searchRequest.From) &&
                   !string.IsNullOrEmpty(searchRequest.To);
        }
    }
}
