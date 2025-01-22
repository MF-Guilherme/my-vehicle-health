namespace MyVehicleHealth.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Vehicle(string name)
    {
        Name = name;
    }
}