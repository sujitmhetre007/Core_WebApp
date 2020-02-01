using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.Models
{
    /// <summary>
    /// DAL to handle CRUD 
    /// </summary>
    public class CoreAppDbContext : DbContext
    {
        // map CLR class with Database Table
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// DbContextOptions class will read the DbContext instance from
        /// Dependency Container and will read the Connection String from the
        /// DbContenxt defined in Dependency Container
        /// </summary>
        public CoreAppDbContext(DbContextOptions<CoreAppDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Navigation with One to Many Relationship and Foreign Key
            modelBuilder.Entity<Product>()
             .HasOne(p => p.Category)
             .WithMany(b => b.Products)
             .HasForeignKey(p => p.CategoryRowId);
        }
    }
}
