using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_Software.Models;

namespace MCS_Software.ViewModel
{
    internal class MainViewModel
    {
        ObservableCollection<Client> Clients { get; set; }
        public Booking CurrentBooking { get; set; }

        public MainViewModel()
        {
            Clients = new ObservableCollection<Client>();

            CurrentBooking = new Booking {
                    Passengers = new ObservableCollection<Passenger>()
                };
        }
    }
}
