using System.ComponentModel.DataAnnotations;

namespace MyVehicleHealth.Dtos;

public class VehicleUpdateDto
{
    [Required]
    public string Name { get; set; }
}