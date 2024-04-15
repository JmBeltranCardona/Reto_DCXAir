using Application.DTOs.Flight;

namespace Application.Contracts.JsonData
{
    public interface IJsonData
    {
        IEnumerable<FlightDto> GetRoutes();
    }
}
