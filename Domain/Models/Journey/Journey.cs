namespace Domain.Models.Journey
{
    public class Journey : Entity.Entity
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? Price { get; set; }
        public bool RouteType { get; set; }
        public List<JourneyFlight.JourneyFlight> JourneyFlight { get; set; }

    }
}
