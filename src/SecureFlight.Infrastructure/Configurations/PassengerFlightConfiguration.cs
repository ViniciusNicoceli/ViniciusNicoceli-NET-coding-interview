using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureFlight.Core.Entities;

namespace SecureFlight.Infrastructure.Configurations;

public class PassengerFlightConfiguration : IEntityTypeConfiguration<PassengerFlight>
{
    public void Configure(EntityTypeBuilder<PassengerFlight> builder)
    {
        builder.HasKey(x => new {x.FlightId, x.PassengerId});

        builder.HasData(new List<PassengerFlight>
        {
            new PassengerFlight
            {
                FlightId = 1,
                PassengerId = "50f0921a-9e5e-4457-b339-07351dbe1b8b"
            },
            new PassengerFlight
            {
                FlightId = 1,
                PassengerId = "ff9272f9-51ce-468d-8ad1-1913c7f789aa"
            },
            new PassengerFlight
            {
                FlightId = 2,
                PassengerId = "21047475-8a0a-4a0f-9f32-a9b9cc7e35d4"
            },
            new PassengerFlight
            {
                FlightId = 2,
                PassengerId = "5dfc7735-f725-4d72-a5ff-c3e472f72592"
            }
        });
    }
}