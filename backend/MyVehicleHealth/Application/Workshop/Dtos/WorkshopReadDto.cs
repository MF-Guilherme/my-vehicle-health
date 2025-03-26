using MyVehicleHealth.Application.Maintenance.Dtos;

namespace MyVehicleHealth.Application.Workshop.Dtos;

public class WorkshopReadDto
{
    public int Id { get; set; }
    public string? CompanyName { get; set; }
    public string MechanicName { get; set; }
    public string Phone { get; set; }
    public List<WorkshopMaintenanceSummaryDto> Maintenances { get; set; } = new();
}

public class WorkshopMaintenanceSummaryDto
{
    public int Id { get; set; }
    public string VehicleName { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public List<ServiceSummaryReadDto> Services { get; set; }
    public decimal TotalCost { get; set; }
}