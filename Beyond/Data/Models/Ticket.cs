using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Beyond.Data.Models
{
    public class Ticket
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public User User { get; set; }
        public string UserId { get; set; }
        public Destination Destination { get; set; }
        public string DestinationId { get; set; }
        public Vehicle Vehicle { get; set; }
        public string VehicleId { get; set; }
        

    }
}
