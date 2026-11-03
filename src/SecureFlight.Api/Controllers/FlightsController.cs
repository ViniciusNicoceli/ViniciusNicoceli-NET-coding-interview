using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Utils;
using SecureFlight.Core.DataTransferObjects;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Core.Mapping;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController(
    IService<Flight> flightService,
    IService<Passenger> passengerService,
    IRepository<PassengerFlight> passengerFlightRepository)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        var flights = await flightService.GetAllAsync();
        return flights.Succeeded
            ? Ok(flights.Result.Select(x => x.ToFlightDto()))
            : Problem(flights.ErrorResult.Message, statusCode: flights.ErrorResult.Code.ToHttpStatusCode(), title: "Error Retrieving Flights");
    }

    [HttpDelete("{flightId:long}/passengers/{passengerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddPassengerToFlight([FromRoute] long flightId, [FromRoute] string passengerId)
    {
        var flight = await flightService.FindAsync(flightId);
        if (!flight.Succeeded)
        {
            return Problem($"Flight with id '{flightId}' was not found", statusCode: flight.ErrorResult.Code.ToHttpStatusCode(), title: "Flight Not Found");
        }

        var passenger = await passengerService.FindAsync(passengerId);
        if (!passenger.Succeeded)
        {
            return Problem($"Passenger with id '{passengerId}' was not found", statusCode: passenger.ErrorResult.Code.ToHttpStatusCode(), title: "Passenger Not Found");
        }

        flight.Result.Passengers.Remove(passenger);
        if(passenger.Result.Flights.Count() == 0)
        {
        }
        
        await passengerFlightRepository.SaveChangesAsync();

        return Ok(passenger.Result.ToPassengerDto());
    }

    [HttpPost("{flightId:long}/passengers/{passengerId}")]
    [ProducesResponseType(typeof(PassengerDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddPassengerToFlight([FromRoute] long flightId, [FromRoute] string passengerId)
    {
        var flight = await flightService.FindAsync(flightId);
        if (!flight.Succeeded)
        {
            return Problem($"Flight with id '{flightId}' was not found", statusCode: flight.ErrorResult.Code.ToHttpStatusCode(), title: "Flight Not Found");
        }

        var passenger = await passengerService.FindAsync(passengerId);
        if (!passenger.Succeeded)
        {
            return Problem($"Passenger with id '{passengerId}' was not found", statusCode: passenger.ErrorResult.Code.ToHttpStatusCode(), title: "Passenger Not Found");
        }

        var existing = await passengerFlightRepository.FilterAsync(pf => pf.FlightId == flightId && pf.PassengerId == passengerId);
        if (existing.Count > 0)
        {
            return Problem($"Passenger '{passengerId}' is already on flight '{flightId}'", statusCode: StatusCodes.Status409Conflict, title: "Passenger Already On Flight");
        }

        await passengerFlightRepository.AddAsync(new PassengerFlight
        {
            FlightId = flightId,
            PassengerId = passengerId
        });
        await passengerFlightRepository.SaveChangesAsync();

        return Ok(passenger.Result.ToPassengerDto());
    }
}