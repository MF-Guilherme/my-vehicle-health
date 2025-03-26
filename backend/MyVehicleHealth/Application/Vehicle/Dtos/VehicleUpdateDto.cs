using System.ComponentModel.DataAnnotations;

namespace MyVehicleHealth.Application.Vehicle.Dtos;

public class VehicleUpdateDto
{
    [Required]
    public string Name { get; set; }
}