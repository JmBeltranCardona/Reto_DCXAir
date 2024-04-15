using Application.DTOs.Flight;
using Application.DTOs.Journey;
using Application.DTOs.JourneyFlight;
using Application.DTOs.Transport;
using Domain.Models.Flight;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;
using AutoMapper;
using Domain.Models.FlightLocations;
using Application.DTOs.FlightLocations;

namespace Application.Mapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Transport, TransportDto>();
            CreateMap<Journey, JourneyDto>();
            CreateMap<Journey, PostJourneyDto>();
            CreateMap<Flight, FlightDto>();
            CreateMap<Flight, PostFlightDto>();
            CreateMap<JourneyFlight, JourneyFlightDto>();
            CreateMap<FlightLocations, FlightLocationsDto>();
        }
    }
}
