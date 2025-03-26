using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Domain.Entities;

namespace MyVehicleHealth.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Suas configurações de relacionamento aqui
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Service> Services { get; set; }
}