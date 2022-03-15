using System;
using System.ComponentModel.DataAnnotations;

namespace Beyond.Data.Models
{
    public class Seat
    {
        public Seat()
        {
            IsTaken=false;
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public int Number { get; set; }
        public bool IsTaken { get; set; }
    }
}