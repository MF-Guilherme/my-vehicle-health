using MyVehicleHealth.Application.Maintenance.Dtos;

namespace MyVehicleHealth.Application.Vehicle.Dtos;

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
    public List<ServiceSummaryReadDto> Services { get; set; } = new List<ServiceSummaryReadDto>();
    public decimal TotalCost { get; set; }
}

