using System.ComponentModel.DataAnnotations;

namespace MyVehicleHealth.Application.Vehicle.Dtos;

public class VehicleCreateDto
{
    [Required]
    public string Name { get; set; }
}