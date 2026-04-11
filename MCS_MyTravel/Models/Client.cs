using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_MyTravel.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string PassportId { get; set; }
        public string Notes {  get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
