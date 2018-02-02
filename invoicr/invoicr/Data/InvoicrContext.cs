using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static invoicr.Models.InvoicrModel;

namespace invoicr.Data
{
    public class InvoicrContext : DbContext
    {

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=dotnetcore.jetstreamsgo.com;database=invoicr;user=library;password=A%b42aWD#A@!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();
                entity.HasMany(e => e.Orders);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Quantity)
                    .IsRequired();
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders);
                entity.HasOne(e => e.Invoice)
                    .WithMany(i => i.Orders);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name)
                    .IsRequired();
                entity.Property(e => e.Price)
                    .IsRequired();
                entity.HasMany(e => e.Orders)
                    .WithOne(o => o.Product);
            });
        }

    }
}
