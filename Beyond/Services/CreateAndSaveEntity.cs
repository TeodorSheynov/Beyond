using System;
using System.Linq;

using Beyond.Data;
using Beyond.Data.DTOs;
using Beyond.Data.Models;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class CreateAndSaveEntity :ICreateAndSaveEntity
    {
        private readonly ITakeEntityById _takeEntityById;
        private readonly ApplicationDbContext _context;
        private readonly IGenerate _generate;
        public CreateAndSaveEntity(ITakeEntityById takeEntityById, ApplicationDbContext context, IGenerate generate)
        {
            _takeEntityById = takeEntityById;
            _context = context;
            _generate = generate;
        }
        public void Vehicle(VehicleDto dto)
        {
            var pilot = _takeEntityById.Pilot(dto.PilotId);
            var seats = _generate.Seats(dto.Seats);
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

        public  void Destination(DestinationDto dto)
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

        public  void Pilot(PilotDto dto)
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

        public void Ticket(User user, Vehicle vehicle)
        {
            var seat = vehicle.Seats.FirstOrDefault(x => x.IsTaken == false);
            if (seat == null) throw new ArgumentNullException();
            vehicle.Seats.FirstOrDefault(s => s.Id == seat.Id)!.IsTaken = true;
            var ticket = new Ticket
            {
                Description = vehicle.Destination.Description,
                ImgPath = vehicle.Destination.Url,
                User = user,
                Vehicle = vehicle,
                Seat = seat.SeatNumber
            };
            _context.Vehicles.Update(vehicle);
            _context.Tickets.Add(ticket); 
            _context.SaveChanges();
        }
    }
}