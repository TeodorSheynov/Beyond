using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beyond.Data.Models
{
    public class Destination
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Distance { get; set; }
        public decimal Price { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();
    }
}
