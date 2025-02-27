namespace MyVehicleHealth.Models;

public class Workshop
{
    public int Id { get; set; }
    public string? CompanyName { get; set; }
    public string MechanicName { get; set; }
    public string Phone { get; set; }

    public Workshop() { }
    
    public Workshop(string mechanicName, string phone)
    {
        MechanicName = mechanicName;
        Phone = phone;
    }
    
    public List<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
}
