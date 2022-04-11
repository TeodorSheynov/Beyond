using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beyond.Data.Models
{
    public class Ticket
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Description { get; set; }
        public virtual User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [Required]
        public string ImgPath { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public string VehicleId { get; set; }

        public int Seat { get; set; }

    }
}
