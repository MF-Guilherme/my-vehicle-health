namespace MyVehicleHealth.Dtos;

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
    public DateTime MaintenanceDate { get; set; }
    public decimal TotalCost { get; set; }
}