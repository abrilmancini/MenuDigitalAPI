using MenuDigitalApi.Models;
using MenuDigitalApi.Repositories;
using MenuDigitalApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MenuDigitalApi.Data
{

    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Restaurant> Restaurants { get; set; }=null!;
        public DbSet<MenuCategory> MenuCategories { get; set; }=null!;
        public DbSet<MenuItem> MenuItems { get; set; }=null!;
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Categories)
                .WithOne(mc => mc.Restaurant)
                .HasForeignKey(mc => mc.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MenuCategory>()
                .HasMany(mc => mc.Items)
                .WithOne(mi => mi.MenuCategory)
                .HasForeignKey(mi => mi.MenuCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
           
         }
    }
}
