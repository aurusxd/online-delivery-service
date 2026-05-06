using System;

namespace DeliveryService.Models
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public string? Status { get; set; }
        public DateTime Created_At { get; set; }
        public string? Feedback { get; set; }

        public List<Order>? Orders { get; set; } = new();


    }
}
