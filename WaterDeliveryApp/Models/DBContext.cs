using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterDeliveryApp.Domain
{
    class ApplicationDBContext : DbContext
    {
        public DbSet<Clients> Clients { get; set; }
        public DbSet<WaterTypes> WaterTypes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderWater> OrderWaters { get; set; }

        public ApplicationDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UseresDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
