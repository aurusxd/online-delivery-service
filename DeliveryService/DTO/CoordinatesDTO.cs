using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DTO
{
    public class CoordinatesDTO
    {
        public string? type { get; set; }

        public List<List<double>>? coordinates { get; set; }
    }
}
