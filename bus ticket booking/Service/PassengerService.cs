using System.Collections.Generic;
using System.Threading.Tasks;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Services
{
    public class PassengerService
    {
        private readonly AppDbContext _context;

        public PassengerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Passenger>> GetAllAsync()
        {
            return await _context.Passengers.Include(p => p.Sales).ToListAsync();
        }

        public async Task<Passenger> GetByIdAsync(int id)
        {
            return await _context.Passengers.Include(p => p.Sales).FirstOrDefaultAsync(p => p.PassengerId == id);
        }

        public async Task AddAsync(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
            }
        }
    }
}
