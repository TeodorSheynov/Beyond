using System;

namespace Beyond.Models
{
    public class TicketViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
        public int TicketsLeft { get; set; }
        public string LaunchSite { get; set; }
    }
}