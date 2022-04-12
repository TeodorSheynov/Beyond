using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Beyond.Models.DTOs.Output
{
    public class EditDestinationViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Distance { get; set; }
        public string Url { get; set; }
    }
}