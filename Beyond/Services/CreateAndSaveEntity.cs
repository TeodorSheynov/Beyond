using System.Collections.Generic;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Models.DTOs;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class CreateAndSaveEntity :ICreateAndSaveEntity
    {
        private readonly ITakeEntityById _takeEntityById;
        private readonly ApplicationDbContext _context;
        public CreateAndSaveEntity(ITakeEntityById takeEntityById, ApplicationDbContext context)
        {
            _takeEntityById = takeEntityById;
            _context = context;
        }
        public void Vehicle(VehicleDto dto)
        {
            var seats = new List<Seat>();
            
            for (var i = 1; i <= dto.Seats; i++)
            {
                var seat=new Seat()
                {
                    IsTaken = false,
                    SeatNumber = i
                };
                seats.Add(seat);
            }

            var pilot = _takeEntityById.Pilot(dto.PilotId);
            var vehicle = new Vehicle
            {
                Arrival = dto.Arrival,
                Departure = dto.Departure,
                Destination = _takeEntityById.Destination(dto.DestinationId),
                DestinationId = dto.DestinationId,
                LaunchSite = dto.LaunchSite,
                Name = dto.Name,
                OnFLight = false,
                Pilot = pilot,
                PilotId = dto.PilotId,
                Seats = seats,
                SerialNumber = dto.SerialNumber,
                Speed = dto.Speed,
            };
            pilot.Vehicle= vehicle;
            _context.Update(pilot);
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void Destination(DestinationDto dto)
        {
            var destination = new Destination
            {
                Name = dto.Name,
                Description = dto.Description,
                Distance = dto.Distance,
                Price = dto.Price,
                Url = dto.Url
            };
            _context.Destinations.Add(destination);
            _context.SaveChanges();
        }

        public void Pilot(PilotDto dto)
        {
            var pilot = new Pilot()
            {
                Age = dto.Age,
                Description = dto.Description,
                ImgPath = dto.Url,
                Rank = dto.Rank,
                Name = dto.Name
            };
            _context.Pilots.Add(pilot);
            _context.SaveChanges();
        }
    }
}