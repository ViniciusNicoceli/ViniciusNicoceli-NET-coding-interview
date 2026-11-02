using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Utils;
using SecureFlight.Core.DataTransferObjects;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Core.Mapping;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController(IService<Flight> flightService)
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
}