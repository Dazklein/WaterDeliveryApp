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

        string connectionString = "Host=localhost;Port=5432;Database=WaterDeliveryDB;Username=postgres;Password=adminpassword";
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
