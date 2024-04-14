namespace Domain.DTOs
{
    public class FlightDTO
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? price { get; set; }
        public TransportDTO Transport { get; set; }
    }
}
