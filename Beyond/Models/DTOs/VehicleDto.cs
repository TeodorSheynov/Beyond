namespace Beyond.Models.DTOs
{
    public class VehicleDto
    {
        public string Model { get; set; }
        public int Speed { get; set; }
        public string PilotId { get; set; }
        public string SerialNumber { get; set; }
        public int Seats { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string DestinationId { get; set; }
    }
}