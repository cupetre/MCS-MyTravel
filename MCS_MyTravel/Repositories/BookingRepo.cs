using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Data;
using MCS_MyTravel.Models;
using Microsoft.EntityFrameworkCore;

namespace MCS_MyTravel.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly AppDbContext _context;

        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetBookingByClientAsync(int clientId)
        {
            return await _context.Bookings
                .Where( b => b.ClientId == clientId )
                .Include( b => b.Passengers )
                .Include( b => b.Payments )
                .OrderBy( b => b.Id )
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {

            if ( id < 0 )
            {
                throw new ArgumentException("Booking ID must be greater than zero.", nameof(id));
            }

            return await _context.Bookings
                .Include(b => b.Passengers)
                .Include(b => b.Payments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateNewBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
