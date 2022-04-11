using System;
using System.ComponentModel.DataAnnotations;

namespace Beyond.Models.DTOs.Input
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
        [Range(1,Int32.MaxValue)]
        public int Distance { get; set; }
        [Required]
        public string Url { get; set; }
    }
}