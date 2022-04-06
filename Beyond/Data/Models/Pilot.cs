using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Beyond.Data.Models.Enums;

namespace Beyond.Data.Models
{
    public class Pilot
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        public string Description { get; set; }
        [Required]
        public Rank Rank { get; set; }
        [Required]
        public string ImgPath { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public string VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

    }
}
