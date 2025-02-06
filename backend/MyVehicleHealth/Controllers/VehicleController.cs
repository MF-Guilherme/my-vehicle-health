using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Data;
using MyVehicleHealth.Dtos;
using MyVehicleHealth.Models;

namespace MyVehicleHealth.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly AppDbContext _context;

    public VehicleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var vehicles = _context.Vehicles
            .Select(v => new VehicleReadDto
            {
                Id = v.Id,
                Name = v.Name,
                Maintenances = v.Maintenances.Select(m => new MaintenanceSummaryDto
                {
                    Id = m.Id,
                    MaintenanceDate = m.MaintenanceDate,
                    WorkshopName = m.Workshop.CompanyName,
                    Services = m.Services.Select(s => new ServiceSummaryReadDto
                    {
                        Id = s.Id,
                        Description = s.Description,
                    }).ToList(),
                    TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost)
                }).ToList()
            })
            .ToList();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var vehicle = _context.Vehicles
            .Where(v => v.Id == id)
            .Select(v => new VehicleReadDto
            {
                Id = v.Id,
                Name = v.Name,
                Maintenances = v.Maintenances.Select(m => new MaintenanceSummaryDto
                {
                    Id = m.Id,
                    MaintenanceDate = m.MaintenanceDate,
                    WorkshopName = m.Workshop.CompanyName,
                    Services = m.Services.Select(s => new ServiceSummaryReadDto
                    {
                        Id = s.Id,
                        Description = s.Description,
                    }).ToList(),
                    TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost)
                }).ToList()
            })
            .FirstOrDefault();
        return vehicle is null ? NotFound() : Ok(vehicle);
    }

    [HttpPost]
    public IActionResult Create(VehicleCreateDto dto)
    {
        var vehicle = new Vehicle
        {
            Name = dto.Name,
        };
        
        _context.Vehicles.Add(vehicle);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, VehicleUpdateDto dto)
    {
        var vehicle = _context.Vehicles.Find(id);
        if (vehicle is null) return NotFound();
        
        vehicle.Name = dto.Name;
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var vehicle = _context.Vehicles.Find(id);
        if (vehicle is null) return NotFound();
        _context.Vehicles.Remove(vehicle);
        _context.SaveChanges();
        return NoContent();
    }
    
}
