using Microsoft.EntityFrameworkCore;
using SkaftoBageriWMS.Api.Models;
using SkaftoBageriWMS.Models;
using System.Collections.Generic;

namespace SkaftoBageriWMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSets for each entity in your database
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configuration if necessary
            // For example, you can set constraints or relationships here:

            // Example configuration for Supplier-Inventory relationship
            modelBuilder.Entity<Inventory>()
                .HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(i => i.ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Define any additional constraints, indexes, or configurations here
        }
    }
}
