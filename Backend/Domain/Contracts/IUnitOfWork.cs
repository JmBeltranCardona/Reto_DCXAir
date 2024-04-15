using Domain.Models.Flight;
using Domain.Models.FlightLocations;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Transport;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Transport> TransportRepository { get; }
        IRepository<Flight> FlightRepository { get; }
        IRepository<Journey> JourneyRepository { get; }
        IRepository<JourneyFlight> JourneyFlightRepository { get; }
        IRepository<FlightLocations> FlightLocationsRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();

    }
}
