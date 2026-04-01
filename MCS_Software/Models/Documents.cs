using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Models
{
    internal class Documents
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
