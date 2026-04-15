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
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext _context;

        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
             _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(Payment payment)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Payment>> GetPaymentsByBookingIdAsync(int bookingId)
        {
            return await _context.Payments
                .Where(p => p.BookingId == bookingId)
                .OrderBy(p => p.BookingId)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.BookingId == id);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
