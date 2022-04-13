using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Models.DTOs.Input;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class CreateEntity :ICreateEntity
    {
        private readonly ITakeEntityById _takeEntityById;
        private readonly ApplicationDbContext _context;
        private readonly IGenerate _generate;
        private readonly IMapper _mapper;
        public CreateEntity(
            ITakeEntityById takeEntityById, 
            ApplicationDbContext context, 
            IGenerate generate, IMapper mapper)
        {
            _takeEntityById = takeEntityById;
            _context = context;
            _generate = generate;
            _mapper = mapper;
        }
        public async Task<int> Vehicle(VehicleDto dto)
        {
            var pilot = _takeEntityById.Pilot(dto.PilotId);
            var seats = _generate.Seats(dto.Seats);
            var vehicle = _mapper.Map<Vehicle>(dto);
            vehicle.Pilot=pilot;
            vehicle.Seats=seats;
            pilot.Vehicle= vehicle;
            _context.Update(pilot);
            _context.Vehicles.Add(vehicle);
          return await _context.SaveChangesAsync();
        }

        public  void Destination(DestinationDto dto)
        {
            var destination = _mapper.Map<Destination>(dto);
             _context.Destinations.Add(destination);
              _context.SaveChanges();
        }

        public  void Pilot(PilotDto dto)
        {
            var pilot = _mapper.Map<Pilot>(dto);
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