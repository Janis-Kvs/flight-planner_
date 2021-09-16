using FlightPlanner.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Web.Storage
{
    public  class AirportStorage
    {
        private readonly List<Airport> _airports;

        public AirportStorage()
        {
            _airports = new List<Airport>();
        }

        public Airport FindAirport(string airportCode)
        {
            return _airports.SingleOrDefault(f => f.AirportCode == airportCode);
        }

        public void PutAirports(List<Airport> airports)
        {
            foreach (Airport airport in airports)
            {
                if (FindAirport(airport.AirportCode) == null)
                {
                    _airports.Add(airport);
                }
            }
        }

        public Airport[] FindAirportByPhrase(string airportPhrase)
        {
            string processedPhrase = airportPhrase.Trim().ToLower();
            int phraseLength = processedPhrase.Length;
            Airport[] airport = _airports.Where(ai =>
                    (ai.AirportCode.Length >= phraseLength && ai.AirportCode.Substring(0, phraseLength).ToLower() == processedPhrase) ||
                    (ai.City.Length >= phraseLength && ai.City.Substring(0, phraseLength).ToLower() == processedPhrase) ||
                    (ai.Country.Length >= phraseLength && ai.Country.Substring(0, phraseLength).ToLower() == processedPhrase)).ToArray();

            return airport;
        }
    }
}
