using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Domain.Entities;

namespace MyVehicleHealth.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Necessário para configurar o Identity

        // Configuração do usuário
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Role).HasDefaultValue("User");
        });

        // Configurar relações (opcional)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.User)
            .WithMany(u => u.Vehicles)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.Vehicle)
            .WithMany(v => v.Maintenances)
            .HasForeignKey(m => m.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.User)
            .WithMany(u => u.Maintenances)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Service>()
            .Property(s => s.LaborCost)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Service>()
            .Property(s => s.PartCost)
            .HasPrecision(18, 2);
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Service> Services { get; set; }
}