using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beyond.Data.Models
{
    public class Destination
    {
        [Key]
        public string Id { get; set; }= Guid.NewGuid().ToString();
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        [Column(TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }
        [Required]
        public  string Url { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();

    }
}
