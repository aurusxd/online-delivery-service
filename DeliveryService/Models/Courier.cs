using System;
using System.Net;

namespace DeliveryService.Models
{
    public class Courier
    {
        public int Id { get; set; }
        public string Nanme { get; set; } = string.Empty;
        public int CourierPhone { get; set; }
        public bool IsActive { get; set; } = false;
        public double Current_Lat { get; set; }
        public double Current_Lon { get; set; }
        public string Vehicle_Type { get; set; } = string.Empty;
        public DateTime Created_At { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
