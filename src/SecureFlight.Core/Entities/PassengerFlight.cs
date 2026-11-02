namespace SecureFlight.Core.Entities;

public class PassengerFlight
{
    public string PassengerId { get; set; }

    public Passenger Passenger { get; set; }

    public long FlightId { get; set; }

    public Flight Flight { get; set; }
}