using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Document
{
    public class Invoice : BaseDocument
    {
        public string InvoiceNumber { get; set; }
        public string PaymentTerms { get; set; }
    }
}
