using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Food?> Foods { get; set; }  = new List<Food?>();
    }
}
