using EShopping.API.Models;

using Microsoft.EntityFrameworkCore;

namespace EShopping.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductProductCategory> ProductProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductCategories)
                .WithMany(e => e.Products)
                .UsingEntity<ProductProductCategory>();
        }
    }
}
