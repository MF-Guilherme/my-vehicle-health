namespace MyVehicleHealth.Domain.Entities;

public class Maintenance
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int VehicleId { get; set; }
    public int WorkshopId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public decimal? TotalCost
    {
        get
        {
            return Services?.Sum(s => s.PartCost + s.LaborCost) ?? 0;
        }
    }

    // Propriedades de navegação (FKs)
    public User User { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Workshop? Workshop { get; set; }
    public List<Service>? Services { get; set; } = new List<Service>();
}