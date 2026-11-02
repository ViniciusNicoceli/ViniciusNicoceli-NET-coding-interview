using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureFlight.Core.Entities;

namespace SecureFlight.Infrastructure.Configurations;

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasData(new List<Passenger>
        {
            new Passenger
            {
                Email = "john.smith@gmail.com",
                FirstName = "John",
                LastName = "Smith",
                Id = "50f0921a-9e5e-4457-b339-07351dbe1b8b"
            },
            new Passenger
            {
                Email = "emerald75@qm1717.com",
                FirstName = "Cecilia",
                LastName = "Hodkiewicz",
                Id = "ff9272f9-51ce-468d-8ad1-1913c7f789aa"
            },
            new Passenger
            {
                Email = "vicente73@xcy1.xyz",
                FirstName = "Muhammad",
                LastName = "Haag",
                Id = "21047475-8a0a-4a0f-9f32-a9b9cc7e35d4"
            },
            new Passenger
            {
                Email = "herman.miguel@mineralka1.cf",
                FirstName = "Torrey",
                LastName = "Kunde",
                Id = "5dfc7735-f725-4d72-a5ff-c3e472f72592"
            }
        });
    }
}