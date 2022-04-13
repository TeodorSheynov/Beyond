using System.Collections.Generic;
using AutoMapper;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Models.DTOs.Input;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class UpdateEntity : IUpdateEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly ITakeEntityById _takeEntityById;
        private readonly IMapper _mapper;
        public UpdateEntity(ApplicationDbContext context, ITakeEntityById takeEntityById, IMapper mapper)
        {
            _context = context;
            _takeEntityById = takeEntityById;
            _mapper = mapper;
        }
        public void Vehicle(VehicleDto dto, string vehicleId)
        {
            var vehicle = _takeEntityById.Vehicle(vehicleId);
            if (vehicle.PilotId == dto.PilotId)
            {
                _mapper.Map(dto, vehicle);
            }
            else
            {
                var pilot = vehicle.Pilot;

                pilot.Vehicle = null;
                pilot.VehicleId = null;
                _mapper.Map(dto, vehicle);
                var newPilot = _takeEntityById.Pilot(vehicle.PilotId);
                newPilot.Vehicle = vehicle;
                newPilot.VehicleId = vehicle.Id;
                _context.Pilots.UpdateRange(new List<Pilot>() {pilot, newPilot});
            }

            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public void Pilot(PilotDto dto, string pilotId)
        {
            var pilot = _takeEntityById.Pilot(pilotId);
            _mapper.Map(dto, pilot);
            _context.Pilots.Update(pilot);
            _context.SaveChanges();
        }

        public void Destination(DestinationDto dto, string destinationId)
        {
            var destination = _takeEntityById.Destination(destinationId);
            _mapper.Map(dto, destination);
            _context.Destinations.Update(destination);
            _context.SaveChanges();
        }
    }
}