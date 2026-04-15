using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_MyTravel.Models
{
    public class Document
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null;

        public DocumentType Type { get; set; }
        public DateTime DateCreated { get; set; }

        public string DocumentNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}