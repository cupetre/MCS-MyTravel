using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_MyTravel.Repositories
{
    public interface IPaymentRepo
    {
        Task<List<Payment>> GetPaymentsByBookingIdAsync(int bookingId);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(Payment payment);
    }
}
