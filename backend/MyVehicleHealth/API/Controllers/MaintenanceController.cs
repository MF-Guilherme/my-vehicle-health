using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Commands;
using MyVehicleHealth.Infrastructure.Data;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Application.Maintenance.Queries;
using MyVehicleHealth.Domain.Entities;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/maintenances")]
public class MaintenanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaintenanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var maintenances = await _mediator.Send(new GetAllMaintenancesQuery());
        return Ok(maintenances);
    }

     [HttpGet("{id}")]
     public async Task<IActionResult> GetById(int id)
     {
         var maintenance = await _mediator.Send(new GetMaintenanceByIdQuery(id));
         return maintenance is null ? NotFound() : Ok(maintenance);
     }

     [HttpPost]
     public async Task<IActionResult> Create([FromBody] MaintenanceCreateDto dto)
     {
         var maintenanceId = await _mediator.Send(new CreateMaintenanceCommand(dto));
         return Created($"/api/maintenances/{maintenanceId}/", new {id = maintenanceId});
     }
     
     [HttpPut("{id}")]
     public async Task<IActionResult> Update(int id, [FromBody] MaintenanceUpdateDto dto)
     {
         await _mediator.Send(new UpdateMaintenanceCommand(id, dto));
         return NoContent();
     }
     
     [HttpDelete("{id}")]
     public async Task<IActionResult> Delete(int id)
     {
         await _mediator.Send(new DeleteMaintenanceCommand(id));
         return NoContent();
     }
}