using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;

namespace Application.Contracts.Algorithm.Search
{
    public interface IDfsRouteSearch
    {
        List<FlightDto> FindRoute(Dictionary<string, List<FlightDto>> graph, string origin, string destination);
    }
}
