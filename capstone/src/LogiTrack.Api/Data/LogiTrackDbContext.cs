using LogiTrack.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Api.Data;

public class LogiTrackDbContext : IdentityDbContext<ApplicationUser>
{
    public LogiTrackDbContext(DbContextOptions<LogiTrackDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.InventoryItem)
            .WithMany(i => i.OrderItems)
            .HasForeignKey(oi => oi.InventoryItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}