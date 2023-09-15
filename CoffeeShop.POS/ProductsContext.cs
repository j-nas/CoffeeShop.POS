using CoffeeShop.POS.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.POS;

public class ProductsContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source=products.db");
}
