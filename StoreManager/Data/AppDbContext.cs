using Microsoft.EntityFrameworkCore;
using StoreManager.Data.Mappings;
using StoreManager.Models;

namespace StoreManager.Data;

public class AppDbContext : DbContext
{
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleProduct> SalesProducts { get; set; }
    public DbSet<Product> Products { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration(new ProductMap());
        mb.ApplyConfiguration(new SaleMap());
        mb.ApplyConfiguration(new SaleProductMap());
    }
}
