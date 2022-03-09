using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Beyond.Data.Models
{
    public class Ticket
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Description { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public Destination Destination { get; set; }
        [ForeignKey(nameof(Destination))]
        public string DestinationId { get; set; }
        public Vehicle Vehicle { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public string VehicleId { get; set; }
        [Required]
        public string ImgPath { get; set; }
        

    }
}
