using Microsoft.AspNetCore.Identity;
    
    namespace MyVehicleHealth.Domain.Entities;
    
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; } = "User";
    
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Maintenance> Maintenances { get; set; }
    }