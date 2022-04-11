using System;
using AutoMapper;
using Beyond.Data.Models;
using Beyond.Models.DTOs.Input;

namespace Beyond
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            this.CreateMap<Vehicle, VehicleDto>().ReverseMap()
                .ForMember(x => x.Seats, opt => opt.Ignore())
                .ForMember(x=>x.Pilot,opt=>opt.Ignore());
            this.CreateMap<DestinationDto, Destination>().ReverseMap();


        }
    }
}