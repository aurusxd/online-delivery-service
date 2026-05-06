using System;

namespace DeliveryService.Models
{
    public class RoutePoint
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public DateTime Recorded_At { get; set; }

        public List<Order>? Orders { get; set; } = new();

    }
}
