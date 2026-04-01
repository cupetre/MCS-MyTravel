using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Models
{
    internal class Payment
    {
        public int Id { get; set; }
        public int AdvancePayment {  get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
