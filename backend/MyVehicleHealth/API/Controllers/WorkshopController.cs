using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyVehicleHealth.Application.Workshop.Dtos;
using MyVehicleHealth.Application.Workshop.Commands;
using MyVehicleHealth.Application.Workshop.Queries;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/workshops")]
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
        var workshopId = await _mediator.Send(new CreateWorkshopCommand(dto));
        return Created($"/api/workshops/{workshopId}", new {id = workshopId});
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] WorkshopUpdateDto dto)
    {
        await _mediator.Send(new UpdateWorkshopCommand(id, dto));
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteWorkshopCommand(id));
        return NoContent();
    }
    
}