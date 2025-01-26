using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Data;
using MyVehicleHealth.Dtos;
using MyVehicleHealth.Models;

namespace MyVehicleHealth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly AppDbContext _context;

    public MaintenanceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var maintenances = _context.Maintenances
            .Include(m => m.Vehicle)
            .Include(m => m.Workshop)
            .Include(m => m.Services)
            .Select(m => new MaintenanceReadDto
            {
                Id = m.Id,
                VehicleName = m.Vehicle.Name,
                WorkshopName = m.Workshop.CompanyName,
                MaintenanceDate = m.MaintenanceDate,
                TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost),
                Services = m.Services.Select(s => s.Description).ToList()
            })
            .ToList();

        return Ok(maintenances);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var maintenance = _context.Maintenances
            .Where(m => m.Id == id)
            .Include(m => m.Vehicle)
            .Include(m => m.Workshop)
            .Include(m => m.Services)
            .Select(m => new MaintenanceReadDto
            {
                Id = m.Id,
                VehicleName = m.Vehicle.Name,
                WorkshopName = m.Workshop.CompanyName,
                MaintenanceDate = m.MaintenanceDate,
                TotalCost = m.Services.Sum(s => s.PartCost + s.LaborCost),
                Services = m.Services.Select(s => s.Description).ToList()
            })
            .FirstOrDefault();

        return maintenance is null ? NotFound() : Ok(maintenance);
    }

    [HttpPost]
    public IActionResult Create(MaintenanceCreateDto dto)
    {
        var maintenance = new Maintenance
        {
            VehicleId = dto.VehicleId,
            WorkshopId = dto.WorkshopId,
            MaintenanceDate = dto.MaintenanceDate,
        };
        
        _context.Maintenances.Add(maintenance);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, maintenance);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, MaintenanceUpdateDto dto)
    {
        var maintenance = _context.Maintenances.Find(id);
        if (maintenance is null) return NotFound();
        
        maintenance.VehicleId = dto.VehicleId;
        maintenance.WorkshopId = dto.WorkshopId;
        maintenance.MaintenanceDate = dto.MaintenanceDate;
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, maintenance);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var maintenance = _context.Maintenances.Find(id);
        if (maintenance is null) return NotFound();
        _context.Maintenances.Remove(maintenance);
        _context.SaveChanges();
        return NoContent();
    }
}