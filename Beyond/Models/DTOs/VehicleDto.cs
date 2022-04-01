using System;
using System.ComponentModel.DataAnnotations;
using Beyond.Helpers.Validation.Attributes;

namespace Beyond.Models.DTOs
{
    
    public class VehicleDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        public string PilotId { get; set; }
        [Required]
        [MaxLength(22,ErrorMessage ="Serial number cant be more than 22 characters")]
        public string SerialNumber { get; set; }
        [Required]
        public int Seats { get; set; }
        [Required]
        public DateTime Departure { get; set; }
        [Required]
        [DateLessThan(nameof(Departure),ErrorMessage = "Arrive date can't be less than departure date")]
        public DateTime Arrival { get; set; }
        [Required]
        public string DestinationId { get; set; }
        [Required]
        public string LaunchSite { get; set; }
    }
}