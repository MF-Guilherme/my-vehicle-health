using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVehicleHealth.Application.Vehicle.Commands;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Application.Vehicle.Queries;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllVehiclesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetVehicleByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VehicleCreateDto dto)
    {
        var vehicle = await _mediator.Send(new CreateVehicleCommand(dto));
        return Created($"/api/vehicles/{vehicle}", new { id = vehicle });
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleUpdateDto dto)
    {
        await _mediator.Send(new UpdateVehicleCommand(id, dto));
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteVehicleCommand(id));
        return NoContent();
    }
}