using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureFlight.Core.Entities;

namespace SecureFlight.Infrastructure.Configurations;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.HasKey(x => x.Code);

        builder.HasMany(x => x.OriginFlights)
            .WithOne(f => f.From)
            .HasForeignKey(f => f.OriginAirport);

        builder.HasMany(x => x.DestinationFlights)
            .WithOne(f => f.To)
            .HasForeignKey(f => f.DestinationAirport);

        builder.HasData(new List<Airport>
        {
            new Airport
            {
                City = "Anapa",
                Country = "Russia",
                Code = "AAQ",
                Name = "Anapa Vityazevo"
            },
            new Airport
            {
                City = "New York",
                Country = "USA",
                Code = "JFK",
                Name = "John F Kennedy International"
            },
            new Airport
            {
                City = "Abadan",
                Country = "Iran",
                Code = "ABD",
                Name = "Abadan"
            },
            new Airport
            {
                City = "Albuquerque",
                Country = "USA",
                Code = "ABQ",
                Name = "Albuquerque International Sunport"
            }
        });
    }
}