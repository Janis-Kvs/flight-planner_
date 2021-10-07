using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IDbServiceExtended : IDbService
    {
        public void DeleteAll<T>() where T : Entity;
    }
}
