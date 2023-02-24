using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartRow>().HasKey(r => new
            {
                r.Id,
                r.CartId

            });
            modelBuilder.Entity<CartRow>().HasOne(r => r.Product).WithMany(r => r.Rows).HasForeignKey(r => r.Id);
            modelBuilder.Entity<CartRow>().HasOne(r => r.Cart).WithMany(r => r.Rows).HasForeignKey(r => r.CartId);
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartRow> CartRows { get; set; }

    }
}
