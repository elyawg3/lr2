using Microsoft.EntityFrameworkCore;
using LunevAPP.Models;

namespace LunevAPP.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
    Database.Migrate();
    }

    // Настройка связей и ограничений
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Связь между заказами и пользователями

        // Связь между заказами и продуктами через OrderItem
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.Id, oi.ProductId });

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.Id);
    }
}
