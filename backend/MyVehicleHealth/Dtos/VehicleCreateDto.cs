using System.ComponentModel.DataAnnotations;

namespace MyVehicleHealth.Dtos;

public class VehicleCreateDto
{
    [Required]
    public string Name { get; set; }
}