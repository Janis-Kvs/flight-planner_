using AutoMapper;
using FlightPlanner.Core.Dto.Requests;
using FlightPlanner.Core.Dto.Responses;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Web2.Mappings
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequest, Flight>()
                    .ForMember(f => f.Id, opt => opt.Ignore());
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(a => a.AirportCode, opt => opt.MapFrom(s => s.Airport))
                    .ForMember( f => f.Id, opt => opt.Ignore());
                cfg.CreateMap<Flight, FlightResponse>();
                cfg.CreateMap<Airport, AirportResponse>()
                    .ForMember(f => f.Airport, opt => opt.MapFrom(s => s.AirportCode));
            });
            // only during development, validate your mappings; remove it before release
            configuration.AssertConfigurationIsValid();
            // use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
            var mapper = configuration.CreateMapper();
            return mapper;
        }
    }
}
