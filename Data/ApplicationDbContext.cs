using baiTaplAnNinh.Models;
using Microsoft.EntityFrameworkCore;
using baiTaplAnNinh.Models;

namespace baiTaplAnNinh.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductComment> ProductComments
            => Set<ProductComment>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductComment>()
                .HasIndex(c => c.ProductId);

            modelBuilder.Entity<ProductComment>()
                .Property(c => c.ProductId)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ProductComment>()
                .Property(c => c.UserName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ProductComment>()
                .Property(c => c.Content)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}