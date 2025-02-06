using Microsoft.AspNetCore.Mvc;
using MyVehicleHealth.Data;
using MyVehicleHealth.Dtos;
using MyVehicleHealth.Models;

namespace MyVehicleHealth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkshopController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkshopController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Workshops.ToList());
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var workshop = _context.Workshops.Find(id);
        return workshop is null ? NotFound() : Ok(workshop);
    }

    [HttpPost]
    public IActionResult Create(WorkshopCreateDto dto)
    {
        var workshop = new Workshop
        {
            CompanyName = dto.CompanyName,
            MechanicName = dto.MechanicName,
            Phone = dto.Phone
        };
        
        _context.Workshops.Add(workshop);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshop);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Workshop updatedWorkshop)
    {
        var workshop = _context.Workshops.Find(id);
        if (workshop is null) return NotFound();
        
        workshop.CompanyName = updatedWorkshop.CompanyName;
        workshop.MechanicName = updatedWorkshop.MechanicName;
        workshop.Phone = updatedWorkshop.Phone;
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = workshop.Id }, workshop);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var workshop = _context.Workshops.Find(id);
        if (workshop is null) return NotFound();
        _context.Workshops.Remove(workshop);
        _context.SaveChanges();
        return NoContent();
    }
    
}