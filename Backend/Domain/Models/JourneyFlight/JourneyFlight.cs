using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.JourneyFlight
{
    public class JourneyFlight : Entity.Entity
    {
        public Guid FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight.Flight? Flight { get; set; }
        public Guid JourneyId { get; set; }
        [ForeignKey("JourneyId")]
        public Journey.Journey? Journey { get; set; }
    }
}
