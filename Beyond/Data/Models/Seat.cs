using System;

namespace Beyond.Data.Models
{
    public class Seat
    {
        public Seat()
        {
            IsTaken = false;
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int SeatNumber { get; set; }
        public bool IsTaken { get; set; }
    }
}