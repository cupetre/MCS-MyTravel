using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Models
{
    internal class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; }
        public decimal TotalPrice { get; set; }
        public ObservableCollection<Passenger> Passengers { get; set; } = new ObservableCollection<Passenger>();
        public string Status { get; set; }   // Draft, Active, Completed, Cancelled
        public string Notes { get; set; }
    }
}
