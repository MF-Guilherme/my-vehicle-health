namespace MyVehicleHealth.Application.Maintenance.Dtos;

public class MaintenanceReadDto
{
    public int Id { get; set; }
    public string VehicleName { get; set; }
    public string WorkshopName { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public decimal TotalCost { get; set; }
    public List<ServiceSummaryReadDto> Services { get; set; }
}

public class ServiceSummaryReadDto
{
    public int Id { get; set; }
    public string Description { get; set; }
}