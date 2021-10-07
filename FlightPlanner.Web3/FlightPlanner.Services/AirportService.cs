using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System.Linq;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(FlightPlannerDbContext context) : base(context)
        {
        }

        public Airport[] FindAirportByPhrase(string airportPhrase)
        {
            string processedPhrase = airportPhrase.Trim().ToLower();
            int phraseLength = processedPhrase.Length;
            Airport[] airport = Query().Where(ai =>
                (ai.AirportCode.Length >= phraseLength && ai.AirportCode.Substring(0, phraseLength).ToLower() == processedPhrase) ||
                (ai.City.Length >= phraseLength && ai.City.Substring(0, phraseLength).ToLower() == processedPhrase) ||
                (ai.Country.Length >= phraseLength && ai.Country.Substring(0, phraseLength).ToLower() == processedPhrase)).ToArray();

            return airport;
        }
    }
}
