using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlightById(int id);
        public bool Exists(Flight flight);
        public void DeleteById(int id);
        public Flight[] SearchFlights(SearchFlightRequest searchFlightRequest);
    }
}
