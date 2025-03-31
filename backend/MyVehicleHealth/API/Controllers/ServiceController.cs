using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Service.Commands;
using MyVehicleHealth.Application.Service.Dtos;
using MyVehicleHealth.Application.Service.Queries;
using MyVehicleHealth.Domain.Entities;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ServiceFilterDto filter)
    {
        var services = await _mediator.Send(new GetAllServicesQuery(filter));
        return Ok(services);

    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult>GetById(int id)
    {
        var service = await _mediator.Send(new GetServiceByIdQuery(id));
        return service is null ? NotFound() : Ok(service);
    
    }
    
    [HttpGet("maintenance/{maintenanceId}")]
    public async Task<IActionResult> GetAllServicesForMaintenance(int maintenanceId)
    {
        var services = await _mediator.Send(new GetAllServicesForMaintenanceQuery(maintenanceId));
        return Ok(services);
    
    }
    
    [HttpPost("maintenance/{maintenanceId}/[controller]")]
    public async Task<IActionResult> Create([FromRoute] int maintenanceId,[FromBody] ServiceCreateDto dto)
    {
        var serviceId = await _mediator.Send(new CreateServiceCommand(maintenanceId, dto));
        return Created($"/api/service/{serviceId}", new { Id = serviceId });
    }
    
    // [HttpPut("{id}")]
    // public IActionResult Update(int id, ServiceUpdateDto dto)
    // {
    //     var service = _context.Services.Find(id);
    //     if (service is null) return NotFound();
    //     
    //     service.Description = dto.Description;
    //     service.MaintenanceDate = dto.MaintenanceDate;
    //     service.CurrentMileage = dto.CurrentMileage;
    //     service.NextMaintenanceDate = dto.NextMaintenanceDate;
    //     service.NextMaintenanceMileage = dto.NextMaintenanceMileage;
    //     service.PartBrand = dto.PartBrand;
    //     service.PartCost = dto.PartCost;
    //     service.LaborCost = dto.LaborCost;
    //     
    //     _context.SaveChanges();
    //     return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    // }
    //
    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     var service = _context.Services.Find(id);
    //     if (service is null) return NotFound();
    //     _context.Services.Remove(service);
    //     _context.SaveChanges();
    //     return NoContent();
    // }
    
    
}