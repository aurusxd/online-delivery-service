using System;

namespace DeliveryService.Models
{

    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string? Address_From { get; set; }
        public string? Address_To { get; set; }
        public int ClientId { get; set; }
        public string? OrderStatus { get; set; }
        public int Courier_Id { get; set; }
        public float Lat_From { get; set; }
        public float Lon_From { get; set; }
        public float Lat_To { get; set; }
        public float Lon_To { get; set; }
        public float Price { get; set; }
        public DateTime Created_at { get; set; }
    }
}
