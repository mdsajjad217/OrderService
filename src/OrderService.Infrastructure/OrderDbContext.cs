using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entity;
using static Confluent.Kafka.ConfigPropertyNames;

namespace OrderService.Infrastructure;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options) { }
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>();
    }
}

