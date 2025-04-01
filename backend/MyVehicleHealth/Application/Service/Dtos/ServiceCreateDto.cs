namespace MyVehicleHealth.Application.Service.Dtos;

public class ServiceCreateDto
{
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int? CurrentMileage { get; set; }
    public DateTime NextMaintenanceDate { get; set; }
    public int? NextMaintenanceMileage { get; set; }
    public string? PartBrand { get; set; }
    public decimal PartCost { get; set; }
    public decimal LaborCost { get; set; }
}