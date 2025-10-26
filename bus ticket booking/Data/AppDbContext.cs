using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using bus_ticket_booking.Models;

namespace bus_ticket_booking.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<sale> Sales { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relasi 1:1 antara Passenger dan Sale
            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.Sale)
                .WithOne(s => s.Passenger)
                .HasForeignKey<sale>(s => s.PassengerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
