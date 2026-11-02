using System.Collections.Generic;

namespace SecureFlight.Core.Entities;

public class FlightStatus
{
    public Enums.FlightStatus Id { get; set; }

    public string Description { get; set; }

    public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
}