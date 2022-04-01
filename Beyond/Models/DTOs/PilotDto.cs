using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Beyond.Data.Models.Enums;

namespace Beyond.Models.DTOs
{
    public class PilotDto
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Required]
        [Range(20,50,ErrorMessage = "The pilot must be within 20 to 50 years.")]
        public int Age { get; set; }
        [StringLength(200,ErrorMessage = "Description must be up to 200 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please choose a Rank.")]
        public Rank Rank { get; set; }
        [Required(ErrorMessage = "Please provide a URL")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}