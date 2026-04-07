using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Document
{
    public class BaseDocument
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string DocumentType { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
