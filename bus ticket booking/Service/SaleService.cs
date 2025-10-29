using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bus_ticket_booking.Data;
using bus_ticket_booking.Models;
using Microsoft.EntityFrameworkCore;

namespace bus_ticket_booking.Services
{
    public class SaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all sales (with related Passenger & Bus)
        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Passenger)
                .Include(s => s.Bus)
                .ToListAsync();
        }

        // ✅ Get sale by ID
        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Passenger)
                .Include(s => s.Bus)
                .FirstOrDefaultAsync(s => s.SaleId == id);
        }

        // ✅ Add new sale (purchase ticket)
        public async Task AddAsync(Sale sale)
        {
            // Ensure bus exists
            var bus = await _context.Buses.FindAsync(sale.BusId);
            if (bus == null)
                throw new Exception("Bus not found.");

            // Ensure enough available seats
            if (bus.AvailableSeats < sale.Quantity)
                throw new Exception("Not enough available seats for this bus.");

            // Update available seats
            bus.AvailableSeats -= sale.Quantity;

            // Calculate total price if not manually set
            if (sale.TotalPrice <= 0)
                sale.TotalPrice = sale.Quantity * bus.TicketPrice;

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        // ✅ Update existing sale
        public async Task UpdateAsync(Sale sale)
        {
            var existingSale = await _context.Sales
                .Include(s => s.Bus)
                .FirstOrDefaultAsync(s => s.SaleId == sale.SaleId);

            if (existingSale == null)
                throw new Exception("Sale not found.");

            // Adjust seat counts if bus or quantity changed
            if (existingSale.BusId != sale.BusId || existingSale.Quantity != sale.Quantity)
            {
                // Return old seats to previous bus
                var oldBus = await _context.Buses.FindAsync(existingSale.BusId);
                if (oldBus != null)
                    oldBus.AvailableSeats += existingSale.Quantity;

                // Deduct new seats from new bus
                var newBus = await _context.Buses.FindAsync(sale.BusId);
                if (newBus == null)
                    throw new Exception("New bus not found.");

                if (newBus.AvailableSeats < sale.Quantity)
                    throw new Exception("Not enough available seats on the new bus.");

                newBus.AvailableSeats -= sale.Quantity;
            }

            // Update sale fields
            existingSale.PassengerId = sale.PassengerId;
            existingSale.BusId = sale.BusId;
            existingSale.Quantity = sale.Quantity;
            existingSale.TotalPrice = sale.TotalPrice;
            existingSale.SaleDate = sale.SaleDate;

            _context.Sales.Update(existingSale);
            await _context.SaveChangesAsync();
        }

        // ✅ Delete sale
        public async Task DeleteAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Bus)
                .FirstOrDefaultAsync(s => s.SaleId == id);

            if (sale == null)
                return;

            // Return seats to bus
            var bus = sale.Bus;
            if (bus != null)
                bus.AvailableSeats += sale.Quantity;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
