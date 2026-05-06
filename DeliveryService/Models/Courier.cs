using System;
using System.Net;

namespace DeliveryService.Models
{
    public class Courier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int OrderId { get; set; }
        public int CourierPhone { get; set; }
        public bool Is_Active { get; set; }
        public float Current_Lat { get; set; }
        public float Current_Lon { get; set; }
        public string? Vehicle_Type { get; set; }

        public List<Order>? Orders { get; set; } = new();
    }
}
