using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureFlight.Core.Entities;
using FlightStatus = SecureFlight.Core.Enums.FlightStatus;

namespace SecureFlight.Infrastructure.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.HasMany(x => x.Passengers)
            .WithMany(f => f.Flights)
            .UsingEntity<PassengerFlight>(
                x => x.HasOne(e => e.Passenger)
                    .WithMany(e => e.PassengerFlights)
                    .HasForeignKey(e => e.PassengerId),
                x => x.HasOne(f => f.Flight)
                    .WithMany(f => f.PassengerFlights)
                    .HasForeignKey(f => f.FlightId)
            );

        builder.HasData(new List<Flight>
        {
            new Flight
            {
                Id = 1,
                ArrivalDateTime = DateTime.Now.AddHours(5),
                Code = "AT001",
                DepartureDateTime = DateTime.Now,
                DestinationAirport = "AAQ",
                OriginAirport = "JFK",
                FlightStatusId = FlightStatus.Active
            },
            new Flight
            {
                Id = 2,
                ArrivalDateTime = DateTime.Now.AddHours(10),
                Code = "AT001",
                DepartureDateTime = DateTime.Now.AddHours(2),
                DestinationAirport = "ABD",
                OriginAirport = "ABQ",
                FlightStatusId = FlightStatus.Active
            }
        });
    }
}