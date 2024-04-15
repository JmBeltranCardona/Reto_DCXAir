using Application.DTOs.Flight;
using Application.DTOs.FlightLocations;
using Application.DTOs.Journey;
using Application.DTOs.JourneyFlight;
using Application.DTOs.Transport;
using AutoMapper;
using Domain.Models.Flight;
using Domain.Models.FlightLocations;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;
using System.Data;

namespace Application.Mapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<TransportDto, Transport>();
            CreateMap<JourneyDto, Journey>();
            CreateMap<PostJourneyDto, Journey>();
            CreateMap<FlightDto, Flight>();
            CreateMap<PostFlightDto, Flight>();
            CreateMap<JourneyFlightDto, JourneyFlight>();
            CreateMap<FlightLocationsDto, FlightLocations>();

        }
    }
}
