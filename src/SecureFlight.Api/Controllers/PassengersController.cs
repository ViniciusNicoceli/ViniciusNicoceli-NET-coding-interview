using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Utils;
using SecureFlight.Core.DataTransferObjects;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Core.Mapping;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengersController(
    IService<Passenger> personService,
    IService<Flight> flightService)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PassengerDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        var passengers = await personService.GetAllAsync();
        return passengers.Succeeded
            ? Ok(passengers.Result.Select(x => x.ToPassengerDto()))
            : Problem(passengers.ErrorResult.Message, statusCode: passengers.ErrorResult.Code.ToHttpStatusCode(), title: "Error Retrieving Passengers");
    }
    
    [HttpGet("/flights/{flightId:long}/passengers")]
    [ProducesResponseType(typeof(IEnumerable<PassengerDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPassengersByFlight(long flightId)
    {
        var flight = await flightService.FindAsync(flightId);
        if (!flight.Succeeded)
        {
            return Problem(flight.ErrorResult.Message, statusCode: flight.ErrorResult.Code.ToHttpStatusCode(), title: "Error Retrieving Passengers for Flight");
        }
        
        var passengers = await personService.FilterAsync(p => p.Flights.Any(x => x.Id == flightId));
        return passengers.Succeeded
            ? Ok(passengers.Result.Select(x => x.ToPassengerDto()))
            : Problem(passengers.ErrorResult.Message, statusCode: passengers.ErrorResult.Code.ToHttpStatusCode(), title: "Error Retrieving Passengers for Flight");
    }
}