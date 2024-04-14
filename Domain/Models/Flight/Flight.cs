using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Flight
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string Origin { get; set; }

        public string Destination { get; set; }

        public double? Price { get; set; }

        public string FlightCarrier { get; set; }

        public string FlightNumber { get; set; }
    }
}
