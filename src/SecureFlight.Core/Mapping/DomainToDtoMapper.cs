using SecureFlight.Core.DataTransferObjects;
using SecureFlight.Core.Entities;

namespace SecureFlight.Core.Mapping;

public static class DomainToDtoMapper
{
    public static AirportDataTransferObject ToAirportDto(this Airport airport) => new(airport.Code, airport.Name, airport.City, airport.Country);
    public static FlightDataTransferObject ToFlightDto(this Flight flight) => new(flight.Id, flight.Code, flight.OriginAirport, flight.DestinationAirport, (int)flight.FlightStatusId, flight.DepartureDateTime, flight.ArrivalDateTime);
    public static PassengerDataTransferObject ToPassengerDto(this Passenger passenger) => new(passenger.Id, passenger.FirstName, passenger.LastName, passenger.Email);
}