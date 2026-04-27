using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_MyTravel.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Notes { get; set; } = string.Empty;

        public Booking Booking { get; set; }
    }
}