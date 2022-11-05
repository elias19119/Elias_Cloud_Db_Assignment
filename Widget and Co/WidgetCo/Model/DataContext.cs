using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Widget_and_Co.Model
{
    public class DataContext : DbContext
    {

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Forum> Forums { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(x => x.Products);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.ProductId);


            modelBuilder.Entity<Forum>()
                .HasOne(x=>x.Product);

        }
    }
}
