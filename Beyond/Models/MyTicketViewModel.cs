using System;

namespace Beyond.Models
{
    public class MyTicketViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Departure { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
    }
}