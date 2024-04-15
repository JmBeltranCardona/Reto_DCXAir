using Domain.Contracts;
using Domain.Models.Flight;
using Domain.Models.FlightLocations;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AplicationDBContext _ctx;
        public IRepository<Transport> TransportRepository => new BaseRepository<Transport>(_ctx);
        public IRepository<Flight> FlightRepository => new BaseRepository<Flight>(_ctx);
        public IRepository<Journey> JourneyRepository => new BaseRepository<Journey>(_ctx);
        public IRepository<JourneyFlight> JourneyFlightRepository => new BaseRepository<JourneyFlight>(_ctx);
        public IRepository<FlightLocations> FlightLocationsRepository => new BaseRepository<FlightLocations>(_ctx);

        public UnitOfWork(AplicationDBContext ctx)
        {
            _ctx = ctx;

        }

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
