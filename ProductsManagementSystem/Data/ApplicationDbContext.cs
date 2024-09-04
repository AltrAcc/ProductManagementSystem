using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;
using ProductsManagementSystem.Models;

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
        public DbSet<InvoiceDetails> InvoicesDetails { get; set; }
        public DbSet<PartyAssignment> PartyAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>().ToTable(nameof(Invoice));
            modelBuilder.Entity<InvoiceDetails>().ToTable(nameof(InvoiceDetails));
            modelBuilder.Entity<PartyAssignment>().ToTable(nameof(PartyAssignment));
            modelBuilder.Entity<Party>().ToTable(nameof(Party));
            modelBuilder.Entity<Product>().ToTable(nameof(Product));
            modelBuilder.Entity<ProductRate>().ToTable(nameof(ProductRate));

            //// Configure relationships and constraints here if needed

            //modelBuilder.Entity<ProductRate>()
            //    .HasKey(pr => new { pr.ProductId, pr.EffectiveDate });


        }
    }
}
