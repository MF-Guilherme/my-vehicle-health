namespace MyVehicleHealth.Dtos;

public class MaintenanceReadDto
{
    public int Id { get; set; }
    public string VehicleName { get; set; }
    public string WorkshopName { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public decimal TotalCost { get; set; }
    public List<string> Services { get; set; }
}