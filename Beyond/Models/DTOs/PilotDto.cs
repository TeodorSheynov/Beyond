using Beyond.Data.Models.Enums;

namespace Beyond.Models.DTOs
{
    public class PilotDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public Rank Rank { get; set; }
        public string Url { get; set; }
    }
}