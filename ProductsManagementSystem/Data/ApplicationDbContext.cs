using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;

namespace ProductsManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Party> Parties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Party>().ToTable("Parties");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductRate>().ToTable("ProductRates");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");

            //// Configure relationships and constraints here if needed

            //modelBuilder.Entity<ProductRate>()
            //    .HasKey(pr => new { pr.ProductId, pr.EffectiveDate });

            //// Add any additional configuration
        }
    }
}
