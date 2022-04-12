using System;
using Beyond.Models.Control;

namespace Beyond.Models.DTOs.Output
{
    public class EditVehicleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public ControlPilotsViewModel Pilot { get; set; }
        public string SerialNumber { get; set; }
        public int Seats { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public ControlDestinationsViewModel Destination { get; set; }
        public string LaunchSite { get; set; }
    }
}