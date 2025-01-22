using Microsoft.AspNetCore.Mvc;
using MyVehicleHealth.Data;
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
    public IActionResult GetAll() => Ok(_context.Vehicles.ToList());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var vehicle = _context.Vehicles.Find(id);
        return vehicle is null ? NotFound() : Ok(vehicle);
    }

    [HttpPost]
    public IActionResult Create(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Vehicle updatedVehicle)
    {
        var vehicle = _context.Vehicles.Find(id);
        if (vehicle is null) return NotFound();
        
        vehicle.Name = updatedVehicle.Name;
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }
    
}
