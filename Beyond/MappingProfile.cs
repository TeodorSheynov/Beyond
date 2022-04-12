﻿using System;
using AutoMapper;
using Beyond.Data.Models;
using Beyond.Models.Destination;
using Beyond.Models.DTOs.Input;
using Beyond.Models.DTOs.Output;

namespace Beyond
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            this.CreateMap<Vehicle, VehicleDto>()
                .ForMember(x=>x.Seats,opt=>opt.MapFrom(s=>s.Seats.Count))
                .ReverseMap()
                .ForMember(x => x.Seats, opt =>opt.Ignore())
                .ForMember(x=>x.Pilot,opt=>opt.Ignore())
                .ForMember(x=>x.Destination,opt=>opt.Ignore());

            this.CreateMap<EditDestinationViewModel, Destination>().ReverseMap();
            this.CreateMap<Destination, DestinationDto>().ReverseMap();

        }
    }
}