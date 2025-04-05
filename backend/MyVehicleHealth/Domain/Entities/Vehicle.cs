namespace MyVehicleHealth.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Vehicle() { }
    
    public Vehicle(string name)
    {
        Name = name;
    }
}