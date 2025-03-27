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
[Route("api/[controller]")]
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
         var maintenance = await _mediator.Send(new CreateMaintenanceCommand(dto));
         return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, maintenance);
     }
     
//     [HttpPut("{id}")]
//     public IActionResult Update(int id, MaintenanceUpdateDto dto)
//     {
//         var maintenance = _context.Maintenances.Find(id);
//         if (maintenance is null) return NotFound();
//         
//         maintenance.VehicleId = dto.VehicleId;
//         maintenance.WorkshopId = dto.WorkshopId;
//         maintenance.MaintenanceDate = dto.MaintenanceDate;
//         _context.SaveChanges();
//         return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, maintenance);
//     }
//     
//     [HttpDelete("{id}")]
//     public IActionResult Delete(int id)
//     {
//         var maintenance = _context.Maintenances.Find(id);
//         if (maintenance is null) return NotFound();
//         _context.Maintenances.Remove(maintenance);
//         _context.SaveChanges();
//         return NoContent();
//     }
}