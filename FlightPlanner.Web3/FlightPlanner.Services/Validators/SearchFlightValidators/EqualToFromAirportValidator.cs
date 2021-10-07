using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators.SearchFlightValidators
{
    public class EqualToFromAirportValidator : ISearchFlightValidator
    {
        public bool IsValidSearchFlight(SearchFlightRequest searchRequest)
        {
            return searchRequest.From.ToUpper() != searchRequest.To.ToUpper();
        }
    }
}
