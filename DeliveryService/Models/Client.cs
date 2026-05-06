using System;

namespace DeliveryService.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Phone { get; set; }
        public string? Email { get; set; }
        public DateTime Created_At { get; set; }
    }
}
    