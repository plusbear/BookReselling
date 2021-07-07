using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new { Id = 1, Name = "Category1" }, 
                new { Id = 2, Name = "Category2" }, 
                new { Id = 3, Name = "Category3" });
            modelBuilder.Entity<Product>().HasData(
                new { Id = 1, Name = "Product1", Description = "Description", SellerId = 1, CategoryId = 1 }, 
                new { Id = 2, Name = "Product2", Description = "", SellerId = 1, CategoryId = 2 }, 
                new { Id = 3, Name = "Product3", Description = "Description", SellerId = 2, CategoryId = 2 }); 
            modelBuilder.Entity<Image>().HasData(
                new { Id = 1, ProductId = 1, ImageName = "google.jpg", ImageRef = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_160x56dp.png"});
        }
    }
}
