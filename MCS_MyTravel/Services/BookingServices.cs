using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_MyTravel.Services
{
    public class BookingServices
    {
        private ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();

        public ObservableCollection<Booking> GetBookings()
        {
            return bookings;
        }

        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }
    }
}
