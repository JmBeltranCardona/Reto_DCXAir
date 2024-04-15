namespace Application.DTOs.Flight
{
    public class PostFlightDto
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? Price { get; set; }
        public Guid TransportId { get; set; }
    }
}
