using System.ComponentModel.DataAnnotations;

namespace Beyond.Models.DTOs
{
    
    public class VehicleDto
    {

        [Required]
        public string Model { get; set; }
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
        [DataType(DataType.DateTime)]
        public string Departure { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public string Arrival { get; set; }
        [Required]
        public string DestinationId { get; set; }
        [Required]
        public string LaunchSite { get; set; }
    }
}