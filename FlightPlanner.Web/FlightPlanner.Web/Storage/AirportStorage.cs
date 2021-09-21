using FlightPlanner.Web.Models;
using System.Linq;
using FlightPlanner.Web.DbContext;

namespace FlightPlanner.Web.Storage
{
    public  class AirportStorage
    {
        public Airport[] FindAirportByPhrase(string airportPhrase, FlightPlannerDbContext context)
        {
            string processedPhrase = airportPhrase.Trim().ToLower();
            int phraseLength = processedPhrase.Length;
            Airport[] airport = context.Airports.Where(ai =>
                    (ai.AirportCode.Length >= phraseLength && ai.AirportCode.Substring(0, phraseLength).ToLower() == processedPhrase) ||
                    (ai.City.Length >= phraseLength && ai.City.Substring(0, phraseLength).ToLower() == processedPhrase) ||
                    (ai.Country.Length >= phraseLength && ai.Country.Substring(0, phraseLength).ToLower() == processedPhrase)).ToArray();

            return airport;
        }
    }
}
