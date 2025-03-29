namespace MyVehicleHealth.Domain.Entities;

public class Service
{
    public int Id { get; set; }
    public int MaintenanceId { get; set; }
    public string Description { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public int? CurrentMileage { get; set; }
    public DateTime NextMaintenanceDate { get; set; }
    public int? NextMaintenanceMileage { get; set; }
    public string? PartBrand { get; set; }
    public decimal PartCost { get; set; }
    public decimal LaborCost { get; set; }

    public Maintenance Maintenance { get; set; }

    public Service() { }

    public Service(int maintenanceId, string description, DateTime maintenanceDate, DateTime nextMaintenanceDate, decimal partCost, decimal laborCost)
    {
        MaintenanceId = maintenanceId;
        Description = description;
        MaintenanceDate = maintenanceDate;
        NextMaintenanceDate = nextMaintenanceDate;
        PartCost = partCost;
        LaborCost = laborCost;
    }
}