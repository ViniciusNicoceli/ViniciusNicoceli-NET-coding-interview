using System.Collections.Generic;

namespace SecureFlight.Core.Entities;

public class Airport
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public ICollection<Flight> OriginFlights { get; set; } = new HashSet<Flight>();

    public ICollection<Flight> DestinationFlights { get; set; } = new HashSet<Flight>();
}