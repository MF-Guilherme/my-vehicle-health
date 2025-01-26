namespace MyVehicleHealth.Dtos;
using MyVehicleHealth.Models;

public class VehicleReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<MaintenanceSummaryDto> Maintenances { get; set; } = new List<MaintenanceSummaryDto>();
}

public class MaintenanceSummaryDto
{
    public int Id { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string WorkshopName { get; set; }
    public decimal TotalCost { get; set; }
}
