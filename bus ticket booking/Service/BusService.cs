using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Services
{
    public class BusService
    {
        private readonly AppDbContext _context;

        public BusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bus>> GetAllAsync()
        {
            return await _context.Buses.ToListAsync();
        }

        public async Task<Bus> GetByIdAsync(int id)
        {
            return await _context.Buses.FirstOrDefaultAsync(b => b.BusId == id);
        }

        public async Task AddAsync(Bus bus)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bus bus)
        {
            _context.Buses.Update(bus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
