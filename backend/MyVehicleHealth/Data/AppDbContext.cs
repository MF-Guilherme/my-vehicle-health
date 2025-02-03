using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Models;

namespace MyVehicleHealth.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Service> Services { get; set; }
}