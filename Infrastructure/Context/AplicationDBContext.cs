using Domain.Models.Flight;
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

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
