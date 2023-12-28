using System;
using Microsoft.EntityFrameworkCore;
using Tienda_CURD.Models;
using Tienda_CURD;

namespace Tienda_CURD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option):base(option)
        {
            
        }   

        public DbSet<Cliente> Clientes {get; set;} = null!;
        public DbSet<Order> Orders {get; set;} = null!;
        public DbSet<OrderDetails> OrderDetails {get; set;} = null!;
        
        protected override void OnModelCreating (ModelBuilder Builder)
        {
            Builder.Entity<Order>()
            .HasMany(o=>o.Details)
            .WithOne(d=>d.Order)
            .HasForeignKey(d=>d.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Tienda_CURD.Product> Product { get; set; } = default!;
    }
}