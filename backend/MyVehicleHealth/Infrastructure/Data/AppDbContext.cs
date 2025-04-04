using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Domain.Entities;

namespace MyVehicleHealth.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Suas configurações de relacionamento aqui
        
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
    public DbSet<User> Users { get; set; }
}