using Application.DTOs.Flight;

namespace Application.DTOs.Graph.Flight
{
    public class FlightGraphDto
    {
        public Dictionary<string, List<FlightDto>> AdjacencyList { get; private set; }

        public FlightGraphDto(List<FlightDto> flights)
        {
            AdjacencyList = new Dictionary<string, List<FlightDto>>();
            foreach (var flight in flights)
            {
                if (!AdjacencyList.ContainsKey(flight.Origin))
                    AdjacencyList[flight.Origin] = new List<FlightDto>();

                AdjacencyList[flight.Origin].Add(flight);
            }
        }
    }
}
