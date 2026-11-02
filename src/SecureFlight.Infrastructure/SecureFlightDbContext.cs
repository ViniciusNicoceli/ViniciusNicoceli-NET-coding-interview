using Microsoft.EntityFrameworkCore;
using SecureFlight.Core.Entities;
using SecureFlight.Infrastructure.Configurations;

namespace SecureFlight.Infrastructure;

public class SecureFlightDbContext(DbContextOptions<SecureFlightDbContext> options)
    : DbContext(options)
{
    public DbSet<Airport> Airports { get; set; }

    public DbSet<Passenger> Passengers { get; set; }
        
    public DbSet<Flight> Flights { get; set; }

    public DbSet<FlightStatus> FlightStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AirportConfiguration());
        modelBuilder.ApplyConfiguration(new FlightConfiguration());
        modelBuilder.ApplyConfiguration(new FlightStatusConfiguration());
        modelBuilder.ApplyConfiguration(new PassengerConfiguration());
        modelBuilder.ApplyConfiguration(new PassengerFlightConfiguration());
    }
}