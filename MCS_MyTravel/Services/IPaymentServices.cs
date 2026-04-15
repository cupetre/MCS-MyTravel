using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_MyTravel.Services
{
    public interface IPaymentServices
    {
        Task<List<Payment>> GetPaymentsByBookingIdAsync(int bookingId);
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(Payment payment);
    }
}
