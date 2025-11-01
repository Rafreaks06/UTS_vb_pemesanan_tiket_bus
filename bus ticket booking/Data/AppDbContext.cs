using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Data {
    public class AppDbContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Sale> Sales { get; set; }

        // Constructor untuk runtime (pakai dependency injection)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ✅ Constructor tambahan untuk design-time (agar Add-Migration bisa jalan)
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // ⚠️ Ganti sesuai konfigurasi database masing-masing
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_vb2_bus_ticketing;Username=postgres;Password=Admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasOne(p => p.Sale).WithOne(s => s.Passenger).HasForeignKey<Sale>(s => s.PassengerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}