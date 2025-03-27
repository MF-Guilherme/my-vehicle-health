using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Application.Vehicle.Queries;
using MyVehicleHealth.Infrastructure.Data;
using MyVehicleHealth.Application.Workshop.Dtos;
using MyVehicleHealth.Domain.Entities;
using System.Threading.Tasks;
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

    // [HttpGet("{id}")]
    // public IActionResult GetById(int id)
    // {
    //     var workshop = _context.Workshops
    //             .Where(w => w.Id == id)
    //             .Include(w => w.Maintenances)
    //             .Select(w => new WorkshopReadDto
    //             {
    //                 Id = w.Id,
    //                 CompanyName = w.CompanyName,
    //                 MechanicName = w.MechanicName,
    //                 Phone = w.Phone,
    //                 Maintenances = w.Maintenances.Select(m => new WorkshopMaintenanceSummaryDto
    //                 {
    //                     Id = m.Id,
    //                     VehicleName = m.Vehicle.Name,
    //                     MaintenanceDate = m.MaintenanceDate,
    //                     Services = m.Services.Select(s => new ServiceSummaryReadDto
    //                     {
    //                         Id = s.Id,
    //                         Description = s.Description,
    //                     }).ToList(),
    //                     TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost),
    //                 }).ToList()
    //             })
    //             .FirstOrDefault();
    //     
    //     return workshop is null ? NotFound() : Ok(workshop);
    // }
    //
    // [HttpPost]
    // public IActionResult Create(WorkshopCreateDto dto)
    // {
    //     var workshop = new Workshop
    //     {
    //         CompanyName = dto.CompanyName,
    //         MechanicName = dto.MechanicName,
    //         Phone = dto.Phone
    //     };
    //     
    //     _context.Workshops.Add(workshop);
    //     _context.SaveChanges();
    //     return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshop);
    // }
    //
    // [HttpPut("{id}")]
    // public IActionResult Update(int id, WorkshopUpdateDto dto)
    // {
    //     var workshop = _context.Workshops.Find(id);
    //     if (workshop is null) return NotFound();
    //     
    //     workshop.CompanyName = dto.CompanyName;
    //     workshop.MechanicName = dto.MechanicName;
    //     workshop.Phone = dto.Phone;
    //     _context.SaveChanges();
    //     
    //     return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshop);
    // }
    //
    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     var workshop = _context.Workshops.Find(id);
    //     if (workshop is null) return NotFound();
    //     _context.Workshops.Remove(workshop);
    //     _context.SaveChanges();
    //     return NoContent();
    // }
    
}