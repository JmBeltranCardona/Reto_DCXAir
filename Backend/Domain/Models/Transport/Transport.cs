namespace Domain.Models.Transport
{
    public class Transport : Entity.Entity
    {
        public string? FlightCarrier { get; set; }
        public string? FlightNumber { get; set; }
    }
}
