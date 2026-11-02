using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Utils;
using SecureFlight.Core.DataTransferObjects;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Core.Mapping;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AirportsController(
    IRepository<Airport> airportRepository,
    IService<Airport> airportService)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AirportDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        var airports = await airportService.GetAllAsync();
        return airports.Succeeded
            ? Ok(airports.Result.Select(x => x.ToAirportDto()))
            : Problem(airports.ErrorResult.Message, statusCode: airports.ErrorResult.Code.ToHttpStatusCode(), title: "Error Retrieving Airports");
    }
    
    [HttpPut("{code}")]
    [ProducesResponseType(typeof(AirportDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Put([FromRoute] string code, AirportDataTransferObject airportDto)
    {
        var airport = await airportRepository.GetByIdAsync(code);
        if (airport is null)
        {
            return Problem($"Airport with code '{code}' was not found", title: "Airport Not Found", statusCode: StatusCodes.Status404NotFound);
        }

        airport.City = airportDto.City;
        airport.Country = airportDto.Country;
        airport.Name = airportDto.Name;
        var result = airportRepository.Update(airport);
        return Ok(result.ToAirportDto());
    }
}