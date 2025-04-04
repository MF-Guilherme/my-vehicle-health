using System.ComponentModel.DataAnnotations;

namespace MyVehicleHealth.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(100)]
    public string Name { get; set; }
    
    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; }
    
    [Required, MaxLength(255)]
    public string PasswordHash { get; set; }
    
    [Required, MaxLength(20)]
    public string Role { get; set; } = "User"; // "Admin" ou "User"
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Relacionamentos (opcional)
    public ICollection<Vehicle> Vehicles { get; set; }
    public ICollection<Maintenance> Maintenances { get; set; }
}