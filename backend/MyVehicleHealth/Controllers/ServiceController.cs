using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Data;
using MyVehicleHealth.Dtos;
using MyVehicleHealth.Models;

namespace MyVehicleHealth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly AppDbContext _context;

    public ServiceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] ServiceFilterDto filter)
    {
        var query = _context.Services
            .Include(s => s.Maintenance)
            .AsQueryable(); // Permite adicionar filtros dinamicamente

        // Filtro por descrição (LIKE)
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(s => s.Description.Contains(filter.Description));
        }
        
        // Filtro por data de manutenção
        if (filter.MaintenanceDate.HasValue)
        {
            query = query.Where(s => s.MaintenanceDate.Date == filter.MaintenanceDate.Value.Date);
        }
        
        // Filtro por data da próxima manutenção
        if (filter.NextMaintenanceDate.HasValue)
        {
            query = query.Where(s => s.NextMaintenanceDate.Date == filter.NextMaintenanceDate.Value.Date);
        }
        
        var services = query
            .Select(s => new ServiceReadDto
            {
                Id = s.Id,
                MaintenanceId = s.MaintenanceId,
                Description = s.Description,
                PartCost = s.PartCost,
                LaborCost = s.LaborCost,
                MaintenanceDate = s.MaintenanceDate,
                NextMaintenanceDate = s.NextMaintenanceDate,
                CurrentMileage = s.CurrentMileage,
                NextMaintenanceMileage = s.NextMaintenanceMileage,
                PartBrand = s.PartBrand
            })
            .ToList();
        return Ok(services);

    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var service = _context.Services
            .Where(s => s.Id == id)
            .Include(s => s.Maintenance)
            .Select(s => new ServiceReadDto
            {
                Id = s.Id,
                MaintenanceId = s.MaintenanceId,
                Description = s.Description,
                PartCost = s.PartCost,
                LaborCost = s.LaborCost,
                MaintenanceDate = s.MaintenanceDate,
                NextMaintenanceDate = s.NextMaintenanceDate,
                CurrentMileage = s.CurrentMileage,
                NextMaintenanceMileage = s.NextMaintenanceMileage,
                PartBrand = s.PartBrand
            })
            .FirstOrDefault();
        return Ok(service);

    }
    
    [HttpGet("maintenance/{maintenanceId}")]
    public IActionResult GetAllServicesForMaintenance(int maintenanceId)
    {
        var services = _context.Services
            .Where(s => s.MaintenanceId == maintenanceId)
            .Include(s => s.Maintenance)
            .Select(s => new ServiceReadDto
            {
                Id = s.Id,
                MaintenanceId = s.MaintenanceId,
                Description = s.Description,
                PartCost = s.PartCost,
                LaborCost = s.LaborCost,
                MaintenanceDate = s.MaintenanceDate,
                NextMaintenanceDate = s.NextMaintenanceDate,
                CurrentMileage = s.CurrentMileage,
                NextMaintenanceMileage = s.NextMaintenanceMileage,
                PartBrand = s.PartBrand
            })
            .ToList();
        return Ok(services);

    }
    
    [HttpPost]
    public IActionResult Create(int maintenanceId, ServiceCreateDto dto)
    {
        var service = new Service
        {
            MaintenanceId = maintenanceId,
            Description = dto.Description,
            MaintenanceDate = dto.MaintenanceDate,
            CurrentMileage = dto.CurrentMileage,
            NextMaintenanceDate = dto.NextMaintenanceDate,
            NextMaintenanceMileage = dto.NextMaintenanceMileage,
            PartBrand = dto.PartBrand,
            PartCost = dto.PartCost,
            LaborCost = dto.LaborCost,
        };
        _context.Services.Add(service);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ServiceUpdateDto dto)
    {
        var service = _context.Services.Find(id);
        if (service is null) return NotFound();
        
        service.Description = dto.Description;
        service.MaintenanceDate = dto.MaintenanceDate;
        service.CurrentMileage = dto.CurrentMileage;
        service.NextMaintenanceDate = dto.NextMaintenanceDate;
        service.NextMaintenanceMileage = dto.NextMaintenanceMileage;
        service.PartBrand = dto.PartBrand;
        service.PartCost = dto.PartCost;
        service.LaborCost = dto.LaborCost;
        
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var service = _context.Services.Find(id);
        if (service is null) return NotFound();
        _context.Services.Remove(service);
        _context.SaveChanges();
        return NoContent();
    }
    
    
}