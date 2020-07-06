using Bar.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bar.Database
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemOrder>()
                .HasOne(io => io.Order)
                .WithMany(o => o.ItemOrderList);
            modelBuilder.Entity<ItemOrder>()
               .HasKey(e => new { e.OrderId, e.ItemId });
        }
        public DbSet<Item> Item { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ItemOrder> ItemOrder { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
