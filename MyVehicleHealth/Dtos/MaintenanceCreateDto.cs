namespace MyVehicleHealth.Dtos;

public class MaintenanceCreateDto
{
    public int VehicleId { get; set; }
    public int WorkshopId { get; set; }
    public DateTime MaintenanceDate { get; set; }
}