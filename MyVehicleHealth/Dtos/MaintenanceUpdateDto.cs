namespace MyVehicleHealth.Dtos;

public class MaintenanceUpdateDto
{
    public int VehicleId { get; set; }
    public int WorkshopId { get; set; }
    public DateTime MaintenanceDate { get; set; }
}