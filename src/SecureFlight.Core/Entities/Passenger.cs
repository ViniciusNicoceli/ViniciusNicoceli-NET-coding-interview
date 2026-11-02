using System.Collections.Generic;

namespace SecureFlight.Core.Entities;

public class Passenger : Person
{
    public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();

    public ICollection<PassengerFlight> PassengerFlights { get; set; } = new HashSet<PassengerFlight>();
}