using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureFlight.Core.Entities;

namespace SecureFlight.Infrastructure.Configurations;

public class FlightStatusConfiguration : IEntityTypeConfiguration<FlightStatus>
{
    public void Configure(EntityTypeBuilder<FlightStatus> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion<int>();

        builder.HasMany(x => x.Flights)
            .WithOne(f => f.Status)
            .HasForeignKey(f => f.FlightStatusId);

        builder.HasData(Enum.GetValues(typeof(Core.Enums.FlightStatus))
            .Cast<Core.Enums.FlightStatus>()
            .Select(e => new FlightStatus
            {
                Id = e,
                Description = e.ToString()
            }));
    }
}