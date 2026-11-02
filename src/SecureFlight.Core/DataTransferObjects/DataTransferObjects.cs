using System;
using System.ComponentModel.DataAnnotations;

namespace SecureFlight.Core.DataTransferObjects;

public record AirportDataTransferObject([Required]string Code, [Required]string Name, [Required]string City, [Required]string Country);

public record FlightDataTransferObject([Required]long Id, [Required]string Code, [Required]string OriginAirport, [Required]string DestinationAirport, [Required]int FlightStatusId, [Required]DateTime DepartureDateTime, [Required]DateTime ArrivalDateTime);

public record PassengerDataTransferObject([Required]string Id, [Required]string FirstName, [Required]string LastName, [Required]string Email);