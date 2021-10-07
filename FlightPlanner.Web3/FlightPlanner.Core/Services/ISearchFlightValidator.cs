using FlightPlanner.Core.Dto.Requests;

namespace FlightPlanner.Core.Services
{
    public interface ISearchFlightValidator
    {
        bool IsValidSearchFlight (SearchFlightRequest searchRequest);
    }
}
