using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_vb2_bus_ticketing;Username=postgres;Password=Admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasMany(p => p.Sales).WithOne(s => s.Passenger).HasForeignKey(s => s.PassengerId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
