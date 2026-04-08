using System.Collections.ObjectModel;
using MCS_Software.Models;

namespace MCS_Software.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public Client CurrentClient { get; set; }
        public Booking CurrentBooking { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        public MainViewModel()
        {
            Clients = new ObservableCollection<Client>();
            Bookings = new ObservableCollection<Booking>();

            CurrentBooking = new Booking
            {
                Passengers = new ObservableCollection<Passenger>(),
                Payments = new ObservableCollection<Payment>()
            };
        }
    }
}
