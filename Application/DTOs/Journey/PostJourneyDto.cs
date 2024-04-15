namespace Application.DTOs.Journey
{
    public class PostJourneyDto
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? Price { get; set; }
        public bool RouteType { get; set; }
    }
}
