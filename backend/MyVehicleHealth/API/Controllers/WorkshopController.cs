using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Application.Vehicle.Queries;
using MyVehicleHealth.Infrastructure.Data;
using MyVehicleHealth.Application.Workshop.Dtos;
using MyVehicleHealth.Domain.Entities;
using System.Threading.Tasks;
using MyVehicleHealth.Application.Workshop.Commands;
using MyVehicleHealth.Application.Workshop.Queries;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkshopController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkshopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var workshops = await _mediator.Send(new GetAllWorkshopsQuery());
        return Ok(workshops);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>GetById(int id)
    {
        var workshop = await _mediator.Send(new GetWorkshopByIdQuery(id));
        return workshop is null ? NotFound() : Ok(workshop);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkshopCreateDto dto)
    {
        var workshop = await _mediator.Send(new CreateWorkshopCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshop);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] WorkshopUpdateDto dto)
    {
        var workshop = await _mediator.Send(new UpdateWorkshopCommand(id, dto));
        return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshop);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteWorkshopCommand(id));
        return NoContent();
    }
    
}