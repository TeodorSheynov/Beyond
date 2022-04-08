using System.ComponentModel.DataAnnotations;

namespace Beyond.Data.DTOs
{
    public class DestinationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public int Distance { get; set; }
        [Required]
        public string Url { get; set; }
    }
}