using Application.Common.Response;
using Application.Contracts.Journey;
using Application.DTOs.Journey;
using MediatR;

namespace Application.Cqrs.Journey.Queries
{
    public class GetRouteQuery : IRequest<Response<JourneyDto>>
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool RouteType { get; set; } = false;
        public string currency { get; set; } = "USD";
    }

    public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, Response<JourneyDto>>
    {
        private readonly IJourneyService _journeyService;
        public GetRouteQueryHandler(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        public async Task<Response<JourneyDto>> Handle(GetRouteQuery request, CancellationToken cancellationToken)
        {
            return await _journeyService.GetRoute(request);
        }
    }
}
