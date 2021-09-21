using System;
using System.Linq;
using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Interface;
using FlightPlanner.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Web.Storage
{
    public static class FlightStorage
    {
        private static readonly AirportStorage _airportStorage = new();

        public static Flight GetFlight(int id, FlightPlannerDbContext context)
        {
            return FindFlight(id, context);
        }

        public static Flight FindFlight(int id, FlightPlannerDbContext context)
        {
            return context.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .SingleOrDefault(f => f.Id == id);
        }

        public static Flight CreateFlight(AddFlightRequest flightRequest)
        {
            
            Flight flight = new Flight
            {
                ArrivalTime = flightRequest.ArrivalTime,
                Carrier = flightRequest.Carrier,
                DepartureTime = flightRequest.DepartureTime,
                From = flightRequest.From,
                To = flightRequest.To
            };

            return flight;
        }

        public static bool IsSameFlight(AddFlightRequest flightRequest, FlightPlannerDbContext context)
        {
            Flight result = context.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .FirstOrDefault(f => f.ArrivalTime == flightRequest.ArrivalTime 
                                     && f.Carrier == flightRequest.Carrier
                                     && f.DepartureTime == flightRequest.DepartureTime
                                     && f.From.AirportCode == flightRequest.From.AirportCode
                                     && f.From.City == flightRequest.From.City
                                     && f.From.Country == flightRequest.From.Country
                                     && f.To.AirportCode == flightRequest.To.AirportCode
                                     && f.To.City == flightRequest.To.City
                                     && f.To.Country == flightRequest.To.Country);

            return result != null;
        }

        public static bool IsValidFlight(AddFlightRequest flightRequest)
        {
            //check for null and empty values
            bool result = String.IsNullOrEmpty(flightRequest.ArrivalTime)
              || String.IsNullOrEmpty(flightRequest.DepartureTime)
              || String.IsNullOrEmpty(flightRequest.Carrier)
              || flightRequest.From == null
              || flightRequest.To == null;

            if (result)
                return false;

            result = String.IsNullOrEmpty(flightRequest.From.AirportCode)
            || String.IsNullOrEmpty(flightRequest.From.City)
            || String.IsNullOrEmpty(flightRequest.From.Country)
            || String.IsNullOrEmpty(flightRequest.To.AirportCode)
            || String.IsNullOrEmpty(flightRequest.To.City)
            || String.IsNullOrEmpty(flightRequest.To.Country);

            if (result)
                return false;

            //check for equal to and from airports
            var airportFrom = flightRequest.From.AirportCode.Trim().ToUpper();
            var airportTo = flightRequest.To.AirportCode.Trim().ToUpper();
            result = airportFrom == airportTo;

            if (result)
                return false;

            //check for strange dates
            DateTime departureTime = Convert.ToDateTime(flightRequest.DepartureTime);
            DateTime arrivalTime = Convert.ToDateTime(flightRequest.ArrivalTime);
            result = arrivalTime < departureTime || arrivalTime == departureTime;

            if (result)
                return false;

            return !result;
        }

        public static void DeleteFlight(int id, FlightPlannerDbContext context)
        {
            Flight flight = FindFlight(id, context);
            
            if (flight != null)
            {
                context.Airports.Remove(flight.To);
                context.Airports.Remove(flight.From);
                context.Flights.Remove(flight);
                context.SaveChanges();
            }
        }

        public static Airport[] FindAirportByPhrase(string airportPhrase, FlightPlannerDbContext context)
        {
            return _airportStorage.FindAirportByPhrase(airportPhrase, context);
        }

        public static IPageResult<Flight> SearchFlight(SearchFlightRequest searchFlightRequest, FlightPlannerDbContext context)
        {
            Flight[] searchedFlights = context.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .Where(fl => 
                fl.From.AirportCode.ToUpper() == searchFlightRequest.From.Trim().ToUpper()
                && fl.To.AirportCode.ToUpper() == searchFlightRequest.To.Trim().ToUpper()
                && fl.DepartureTime.Substring(0,10) == searchFlightRequest.DepartureDate).ToArray();

            IPageResult<Flight> result = new PageResult(searchedFlights);

            return result;
        }

        public static bool IsValidSearchFlightRequest(SearchFlightRequest searchFlightRequest)
        {
            //check for null and empty values
            bool result = String.IsNullOrEmpty(searchFlightRequest.DepartureDate)
                          || String.IsNullOrEmpty(searchFlightRequest.From) 
                          || String.IsNullOrEmpty(searchFlightRequest.To);

            if (result)
                return false;

            //check for equal to and from airports
            string airportFrom = searchFlightRequest.From.ToUpper();
            string airportTo = searchFlightRequest.To.Trim().ToUpper();
            result = airportFrom == airportTo;

            if (result)
                return false;

            return !result;
        }
    }
}
