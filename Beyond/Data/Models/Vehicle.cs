using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beyond.Data.Models
{
    public class Vehicle
    {
        [Key]
        public string Id { get; set; }=Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Seats { get; set; }

        public ICollection<Destination> Destinations { get; set; }
    }
}