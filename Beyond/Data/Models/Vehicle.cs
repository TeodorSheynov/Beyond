using System;
using System.Collections.Generic;

namespace Beyond.Data.Models
{
    public class Vehicle
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Seats { get; set; }
        public ICollection<Destination> Destinations { get; set; }
    }
}