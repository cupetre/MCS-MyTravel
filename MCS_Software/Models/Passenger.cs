using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Models
{
    public class Passenger
    {
        public int Id {  get; set; }
        public string FullName {  get; set; }
        public DateTime BirthDate { get; set;  }
        public int BookingId {  get; set; }
        public string PassportId {  get; set; }

        public Booking Booking { get; set; }
    }
}
