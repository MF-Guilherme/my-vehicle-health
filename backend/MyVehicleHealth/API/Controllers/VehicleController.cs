using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVehicleHealth.Application.Vehicle.Commands;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Application.Vehicle.Queries;
using System.Threading.Tasks;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleUpdateDto dto)
    {
        var vehicle = await _mediator.Send(new UpdateVehicleCommand(id, dto));
        return Ok(vehicle);
    }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     await _mediator.Send(new DeleteVehicleCommand(id));
    //     return NoContent();
    // }
}