using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DTO
{
    public class AddressDTO
    {
        public string type { get; set; } = string.Empty;
        public double lat { get; set; }
        public double lon { get; set; }
        public string address { get; set; } = string.Empty;

    }
}
