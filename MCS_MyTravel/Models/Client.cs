using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_MyTravel.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        public string? Phone { get; set; }
        
        [Required]
        public string PassportId { get; set; }
        public string? Notes {  get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
