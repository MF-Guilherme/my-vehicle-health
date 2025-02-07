namespace MyVehicleHealth.Dtos;

public class ServiceFilterDto
{
    public string? Description { get; set; }
    public DateTime? MaintenanceDate { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
}