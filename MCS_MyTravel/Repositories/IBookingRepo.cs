using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_MyTravel.Repositories
{
    public interface IBookingRepo
    {
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<List<Booking>> GetBookingByClientAsync(int clientId);
        Task AddBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
        Task UpdateNewBookingAsync(Booking booking);
    }
}
