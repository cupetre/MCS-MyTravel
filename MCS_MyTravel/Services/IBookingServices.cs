using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_MyTravel.Services
{
    public interface IBookingServices
    {
        Task<List<Booking>> GetBookingsByClientIdAsync(int clientId);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);
    }
}
