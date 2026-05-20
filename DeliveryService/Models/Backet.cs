using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
