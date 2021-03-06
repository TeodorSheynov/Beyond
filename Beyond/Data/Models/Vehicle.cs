using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beyond.Data.Models
{
    public class Vehicle
    {
        [Key]
        public string Id { get; set; }=Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public int Speed { get; set; }
        public virtual Pilot Pilot { get; set; }
        [ForeignKey(nameof(Pilot))]
        public string PilotId { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        [Required]
        public virtual Destination Destination { get; set; }
        [ForeignKey(nameof(Destination))]
        public string DestinationId { get; set; }

        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public string LaunchSite { get; set; }
        public bool OnFLight { get; set; }
    }
}