using Application.Common.Response;
using Application.Cqrs.Journey.Queries;
using Application.DTOs.Journey;

namespace Application.Contracts.Journey
{
    public interface IJourneyService
    {
        Task<Response<JourneyDto>> GetRoute(GetRouteQuery getRouteQuery);
    }
}
