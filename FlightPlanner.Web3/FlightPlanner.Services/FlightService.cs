using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(FlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return Query()
                .Include(a => a.To)
                .Include(a => a.From)
                .Any(f => f.ArrivalTime == flight.ArrivalTime
                                     && f.Carrier == flight.Carrier
                                     && f.DepartureTime == flight.DepartureTime
                                     && f.From.AirportCode == flight.From.AirportCode
                                     && f.From.City == flight.From.City
                                     && f.From.Country == flight.From.Country
                                     && f.To.AirportCode == flight.To.AirportCode
                                     && f.To.City == flight.To.City
                                     && f.To.Country == flight.To.Country);
        }

        public void  DeleteById(int id)
        {
            Flight flightToRemove = _context.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .SingleOrDefault(f => f.Id == id);

            if (flightToRemove != null)
            {
                _context.Airports.Remove(flightToRemove.From);
                _context.Airports.Remove(flightToRemove.To);
                _context.Flights.Remove(flightToRemove);
                _context.SaveChanges();
            }
        }

        public Flight[] SearchFlights(SearchFlightRequest searchFlightRequest)
        {
            Flight[] searchedFlights = _context.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .Where(fl =>
                    fl.From.AirportCode.ToUpper() == searchFlightRequest.From.Trim().ToUpper()
                    && fl.To.AirportCode.ToUpper() == searchFlightRequest.To.Trim().ToUpper()
                    && fl.DepartureTime.Substring(0, 10) == searchFlightRequest.DepartureDate).ToArray();

            return searchedFlights;
        }
    }
}
