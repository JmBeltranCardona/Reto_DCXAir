using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Contracts.Algorithm.Search;
using Application.Contracts.ConversionCurrency;
using Application.Contracts.Journey;
using Application.Contracts.JsonData;
using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;
using Application.DTOs.Journey;
using Application.DTOs.JourneyFlight;
using Application.DTOs.Transport;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Flight;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Application.Services.Journey
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonData _JsonData;
        private readonly IBfsRouteSearch _dfsRouteSearch;
        private readonly IMapper _autoMapper;
        private readonly IConversionCurrencyService _conversionCurrencyService;
        private readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public JourneyService(IUnitOfWork unitOfWork, IJsonData jsonData, IBfsRouteSearch dfsRouteSearch, IMapper mapper, 
            IConversionCurrencyService conversionCurrencyService)
        {
            _unitOfWork = unitOfWork;
            _JsonData = jsonData;
            _dfsRouteSearch = dfsRouteSearch;
            _autoMapper = mapper;
            _conversionCurrencyService = conversionCurrencyService;
        }

        public async Task<Response<JourneyDto>> GetRoute(GetRouteQuery getRouteQuery)
        {
            var response = new Response<JourneyDto>();

            try
            {
                if (getRouteQuery.Destination.ToUpper().Equals(getRouteQuery.Origin.ToUpper()))
                {
                    throw new BadRequestException("La Ciudad de Origen y Destino deben ser diferentes");
                }

                var journeyFlight = _unitOfWork.JourneyRepository.Get()
                                    .Where(j => j.Destination == getRouteQuery.Destination.ToUpper())
                                    .Where(j => j.Origin == getRouteQuery.Origin.ToUpper())
                                    .Where(j => j.RouteType == getRouteQuery.RouteType)
                                    .Include(jf => jf.JourneyFlight)
                                    .ThenInclude(f => f.Flight)
                                    .ThenInclude(t => t.Transport)
                                    .FirstOrDefault();

                var journeyRoute = new List<JourneyDto>();
                var journeyResult = new JourneyDto();

                if (journeyFlight == null)
                {
                    var listJourney = _JsonData.GetRoutes().ToList();

                    var graphListJourney = GetGraph(listJourney);

                    journeyResult = await GetRoute(getRouteQuery.Origin, getRouteQuery.Destination, graphListJourney);

                    journeyRoute.Add(journeyResult);

                    // Si no es un viaje de ida, agrega la ruta de vuelta
                    if (!getRouteQuery.RouteType)
                    {
                        journeyResult = await GetRoute(getRouteQuery.Destination, getRouteQuery.Origin, graphListJourney);
                        journeyRoute.Add(journeyResult);
                    }

                    await SaveRoutes(journeyRoute, getRouteQuery.RouteType);

                }
                else
                {
                    var FligthsList = journeyFlight.JourneyFlight.Select(x => x.Flight).ToList();
                    var journey = new JourneyDto
                    {
                        Origin = journeyFlight.Origin,
                        Destination = journeyFlight.Destination,
                        Price = journeyFlight.Price,
                        Flights = _autoMapper.Map<List<FlightDto>>(FligthsList)
                    };

                    journeyRoute.Add(journey);
                }

                if (getRouteQuery.currency != "USD")
                {
                    response.Data = await TransformResponse(journeyRoute, getRouteQuery.currency);
                }
                else
                {
                    response.Data = journeyRoute;
                }

                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al realizar la consulta, Por favor comunicate con el area administrativa. {ex.Message} ";
                _logger.Error("Error al realizar la consulta, Por favor comunicate con el area administrativa ", ex.Message);
                throw new BadRequestException($"Error al consultar el registro del vuelo. {ex.Message} ");
            }


            return response;
        }

        private async Task<List<JourneyDto>?> TransformResponse(List<JourneyDto> listRoutes, string currency)
        {
            var transformedRoutes = new List<JourneyDto>();

            foreach (var route in listRoutes)
            {
                var convertedFlights = new List<FlightDto>();
                double totalPrice = 0;

                foreach (var flight in route.Flights)
                {
                    var convertedPrice = flight.Price;

                    if (currency != "USD")
                    {
                        var convertedAmount = await ConvertPriceAsync((double)flight.Price, currency);
                        if (convertedAmount.HasValue)
                        {
                            convertedPrice = convertedAmount.Value;
                        }
                    }

                    var convertedFlight = new FlightDto
                    {
                        Origin = flight.Origin,
                        Destination = flight.Destination,
                        Price = convertedPrice,
                        Transport = flight.Transport
                    };

                    convertedFlights.Add(convertedFlight);

                    totalPrice += (double)convertedPrice;
                }

                var transformedRoute = new JourneyDto
                {
                    Origin = route.Origin,
                    Destination = route.Destination,
                    Price = totalPrice,
                    Flights = convertedFlights
                };

                transformedRoutes.Add(transformedRoute);
            }

            return transformedRoutes;
        }

        private Dictionary<string, List<FlightDto>> GetGraph(List<FlightDto> flightDTO)
        {
            var graph = new Dictionary<string, List<FlightDto>>();

            foreach (var flight in flightDTO)
            {
                if (!graph.ContainsKey(flight.Origin))
                {
                    graph[flight.Origin] = new List<FlightDto>();
                }
                graph[flight.Origin].Add(flight);
            }
            return graph;
        }

        public async Task<JourneyDto> GetRoute(string origin, string destination, Dictionary<string, List<FlightDto>> listRoutes)
        {
            var result = new List<FlightDto>();


            result = _dfsRouteSearch.FindRoute(listRoutes, origin, destination);


            var TotalPrice = result.Select(r => r.Price).Sum();

            var Journey = new JourneyDto
            {
                Origin = origin,
                Destination = destination,
                Price = TotalPrice,
                Flights = result
            };

            return Journey;
        }

        private async Task<bool> SaveRoutes(List<JourneyDto> journeyDto, bool routeType)
        {
            try
            {
                bool allRoutesSaved = true;

                foreach (var journey in journeyDto)
                {
                    var dataJourney = new PostJourneyDto
                    {
                        Origin = journey.Origin,
                        Destination = journey.Destination,
                        Price = journey.Price,
                        RouteType = routeType
                    };

                    var mapJourney = _autoMapper.Map<Domain.Models.Journey.Journey>(dataJourney);
                    var journeyAdd = await _unitOfWork.JourneyRepository.Add(mapJourney);

                    var response = await StorageFlightsInformation(journey, journeyAdd.Id);

                    if (!response)
                    {
                        allRoutesSaved = false;
                    }
                }

                return allRoutesSaved;
            }
            catch (Exception ex)
            {
                _logger.Error("Ocurrio un error almacenando las rutas en la base de datos, Error al realizar la consulta  ", ex.Message);
                throw new BadRequestException($"Ocurrio un error almacenando las rutas en la base de datos, Error al realizar la consulta, Por favor comunicate con el area administrativa. {ex.Message} ");
            }
        }

        private async Task<bool> StorageFlightsInformation(JourneyDto journeyDto, Guid JourneyId)
        {
            try
            {
                foreach (var itemFlight in journeyDto.Flights)
                {
                    var transportDto = new TransportDto
                    {
                        FlightCarrier = itemFlight.Transport.FlightCarrier,
                        FlightNumber = itemFlight.Transport.FlightNumber
                    };

                    var transport = await _unitOfWork.TransportRepository.Add(_autoMapper.Map<Transport>(transportDto));

                    var flightDto = new PostFlightDto
                    {
                        Origin = itemFlight.Origin,
                        Destination = itemFlight.Destination,
                        Price = itemFlight.Price,
                        TransportId = transport.Id
                    };

                    var flight = await _unitOfWork.FlightRepository.Add(_autoMapper.Map<Flight>(flightDto));

                    var journeyFlightDto = new JourneyFlightDto
                    {
                        JourneyId = JourneyId,
                        FlightId = flight.Id,
                    };

                    await _unitOfWork.JourneyFlightRepository.Add(_autoMapper.Map<JourneyFlight>(journeyFlightDto));
                }

                return true;

            }
            catch (Exception ex)
            {
                _logger.Error("Ocurrio un error almacenando las rutas en la base de datos", ex.Message);
                throw new BadRequestException($"Ocurrio un error almacenando las rutas en la base de datos, Por favor comunicate con el area administrativa. {ex.Message} ");
            }

        }

        private async Task<double?> ConvertPriceAsync(double amount, string targetCurrency)
        {

            if (!string.IsNullOrEmpty(targetCurrency) && targetCurrency != "USD")
            {
                return await _conversionCurrencyService.ConvertCurrency("USD", targetCurrency, amount);
            }
            return amount;
        }
    }
}
