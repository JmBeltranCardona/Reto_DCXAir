using Domain.Models.Flight;
using Domain.Models.FlightLocations;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;
using Infrastructure.JsonData.Contracts;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) 
            : base(options)
        {
        }

        public DbSet<Transport> Transport { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Journey> Journey { get; set; }
        public DbSet<JourneyFlight> JourneyFlight { get; set; }
        public DbSet<FlightLocations> FlightLocations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
