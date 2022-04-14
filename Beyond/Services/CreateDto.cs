using System;
using System.Linq;
using AutoMapper;
using Beyond.Data;
using Beyond.Models.DTOs.Input;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Beyond.Services
{
    public class CreateDto:ICreateDto
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITakeEntityById _takeEntityById;
        public CreateDto(
            ApplicationDbContext context, 
            IMapper mapper, ITakeEntityById takeEntityById)
        {
            _context = context;
            _mapper = mapper;
            _takeEntityById = takeEntityById;
        }
        public VehicleDto Vehicle(string id)
        {
            _takeEntityById.Vehicle(id);
            var dto = _context
                .Vehicles
                .Where(x => x.Id == id)
                .ToArray()
                .Select(x => _mapper.Map<VehicleDto>(x))
                .Single();
            return dto;
        }

        public PilotDto Pilot(string id)
        {
            var dto = _context
                .Pilots
                .Where(x => x.Id == id)
                .ToArray()
                .Select(x => _mapper.Map<PilotDto>(x))
                .Single();
            return dto;
        }

        public DestinationDto Destination(string id)
        {
            var dto = _context
                .Destinations
                .Where(x => x.Id == id)
                .ToArray()
                .Select(d => _mapper.Map<DestinationDto>(d))
                .Single();
            return dto;
        }
    }
}