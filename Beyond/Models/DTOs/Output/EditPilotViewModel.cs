using System.ComponentModel.DataAnnotations;
using Beyond.Data.Models.Enums;

namespace Beyond.Models.DTOs.Output
{
    public class EditPilotViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Rank { get; set; }
        public string Url { get; set; }
    }
}