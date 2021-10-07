using FlightPlanner.Core.Dto.Requests;

namespace FlightPlanner.Core.Services
{
    public interface IFlightValidator
    {
        bool IsValidFlight (FlightRequest request);
    }
}
