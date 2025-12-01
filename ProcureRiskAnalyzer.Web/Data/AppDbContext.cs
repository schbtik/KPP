using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Models;

namespace ProcureRiskAnalyzer.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Tender relationships
            modelBuilder.Entity<Tender>()
                .HasOne(t => t.Supplier)
                .WithMany()
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tender>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tenders)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Microsoft", Country = "USA" },
                new Supplier { Id = 2, Name = "EPAM", Country = "Ukraine" },
                new Supplier { Id = 3, Name = "SoftServe", Country = "Poland" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "IT Services", Description = "Information Technology Services" },
                new Category { Id = 2, Name = "Healthcare", Description = "Healthcare and Medical Services" },
                new Category { Id = 3, Name = "Infrastructure", Description = "Infrastructure and Construction" }
            );

            modelBuilder.Entity<Tender>().HasData(
                new Tender { Id = 1, TenderCode = "T001", Buyer = "Міністерство освіти", Date = new DateTime(2025, 10, 10), ExpectedValue = 1000000, SupplierId = 1, CategoryId = 1 },
                new Tender { Id = 2, TenderCode = "T002", Buyer = "Міністерство охорони здоров'я", Date = new DateTime(2025, 10, 15), ExpectedValue = 750000, SupplierId = 2, CategoryId = 2 }
            );

            // Note: User seed data will be added via migration or initialization
        }
    }
}
