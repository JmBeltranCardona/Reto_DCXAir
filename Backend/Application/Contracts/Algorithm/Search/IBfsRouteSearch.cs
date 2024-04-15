using Application.Cqrs.Journey.Queries;
using Application.DTOs.Flight;

namespace Application.Contracts.Algorithm.Search
{
    public interface IBfsRouteSearch
    {
        List<FlightDto> FindRoute(Dictionary<string, List<FlightDto>> graph, string origin, string destination);
    }
}
