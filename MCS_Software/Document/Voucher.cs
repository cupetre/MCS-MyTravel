using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS_Software.Document
{
    public class Voucher : BaseDocument
    {
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public string CheckInNote { get; set; }
    }
}
