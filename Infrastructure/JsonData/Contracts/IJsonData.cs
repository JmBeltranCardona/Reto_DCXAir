using Domain.DTOs;

namespace Infrastructure.JsonData.Contracts
{
    public interface IJsonData
    {
        IEnumerable<FlightDTO> GetRoutes();
    }
}
