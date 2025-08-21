using Microsoft.EntityFrameworkCore;
using ProductService.Dal.Entities;

namespace ProductService.Dal.Context;
public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Products> Products { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Products>().HasKey(p => p.ProductId);
        //modelBuilder.Entity<Products>().Property(p => p.ProductName).IsRequired().HasMaxLength(100);
        //modelBuilder.Entity<Products>().Property(p => p.Category).HasMaxLength(50);
        //modelBuilder.Entity<Products>().Property(p => p.UnitPrice).HasPrecision(18, 2);
        //modelBuilder.Entity<Products>().Property(p => p.QuantityInStock).IsRequired(false);
    }
}
