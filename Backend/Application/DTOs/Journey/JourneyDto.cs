
using Application.DTOs.Flight;

namespace Application.DTOs.Journey
{
    public class JourneyDto
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? Price { get; set; }
        public List<FlightDto> Flights { get; set; }
    }
}
