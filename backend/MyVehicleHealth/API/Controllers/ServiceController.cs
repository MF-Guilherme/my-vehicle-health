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
[Route("api/services")]
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
    
    [HttpPost("maintenance/{maintenanceId}")]
    public async Task<IActionResult> Create([FromRoute] int maintenanceId,[FromBody] ServiceCreateDto dto)
    {
        var serviceId = await _mediator.Send(new CreateServiceCommand(maintenanceId, dto));
        return Created($"/api/services/{serviceId}", new { Id = serviceId });
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceUpdateDto dto)
    {
        await _mediator.Send(new UpdateServiceCommand(id, dto));
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteServiceCommand(id));
        return NoContent();
    }
    
    
}