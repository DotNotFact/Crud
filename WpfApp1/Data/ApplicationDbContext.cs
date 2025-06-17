using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;

namespace WpfApp1.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
         
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Uid);
            entity.Property(e => e.Uid)
                .IsRequired()
                .ValueGeneratedNever();
            
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        });
         
        modelBuilder.Entity<Product>().HasData(
            new Product { Uid = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Ноутбук", Description = "Игровой ноутбук", Price = 85000, Quantity = 5, CreatedDate = new DateTime(2024, 1, 1) },
            new Product { Uid = new Guid("22222222-2222-2222-2222-222222222222"), Name = "Мышь", Description = "Беспроводная мышь", Price = 2500, Quantity = 15, CreatedDate = new DateTime(2024, 1, 2) },
            new Product { Uid = new Guid("33333333-3333-3333-3333-333333333333"), Name = "Клавиатура", Description = "Механическая клавиатура", Price = 7500, Quantity = 8, CreatedDate = new DateTime(2024, 1, 3) }
        );
    }
}
