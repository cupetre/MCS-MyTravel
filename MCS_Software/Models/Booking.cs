using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }   // Draft, Active, Completed, Cancelled
        public string Notes { get; set; }

        public decimal HotelPrice { get; set; }
        public bool IncludeTravelPrice { get; set; }
        public decimal TravelPrice { get; set; }
        public bool IncludeInsurancePrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public bool IncludeTaxesPrice { get; set; }
        public decimal TaxesPrice { get; set; }

        public ObservableCollection<Passenger> Passengers { get; set; } = new ObservableCollection<Passenger>();
        public ObservableCollection<Payment> Payments { get; set; } = new ObservableCollection<Payment>();
        public decimal TotalPrice
        {
            get
            {
                decimal total = HotelPrice;

                if (IncludeTravelPrice)
                    total += TravelPrice;

                if (IncludeInsurancePrice)
                    total += InsurancePrice;

                if (IncludeTaxesPrice)
                    total += TaxesPrice;

                return total;
            }
        }
    }
}
