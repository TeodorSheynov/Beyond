using System;
using System.Collections.Generic;
using System.Linq;
using Beyond.Data.Models;
using Beyond.Data.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Beyond.Data
{
    public class AppDbInitializer
    {
        private static List<Seat> SeedSeats(int count)
        {
            var seats = new List<Seat>();
            for (int i = 1; i <= count; i++)
            {
                seats.Add(new Seat()
                {
                    IsTaken = false,
                    SeatNumber = i
                });
            }

            return seats;
        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope=applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
            var seatsVehicleOne = 7;
            var seatsVehicleTwo = 4;
            if (!context.Pilots.Any())
            {
                context.Pilots.AddRange(new List<Pilot>()
                {
                    new Pilot()
                    {
                        Id="1",
                        Age = 34,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot1.jpg",
                        Name = "Colby Smith",
                        Rank = Rank.CadetTrainee
                    },
                    new Pilot()
                    {   Id = "2",
                        Age = 43,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot2.jpg",
                        Name = "John Smith",
                        Rank = Rank.SecondOfficer
                    },
                    new Pilot()
                    {
                        Id = "3",
                        Age = 44,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot3.jpg",
                        Name = "Satoshi Nakamoto",
                        Rank = Rank.SeniorFirstOfficer
                    },
                    new Pilot()
                    {
                        Id = "4",
                        Age = 34,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot4.jpg",
                        Name = "Lacy Colt",
                        Rank = Rank.SeniorFirstOfficer
                    }
                });
                context.SaveChanges();
            }
            if (!context.Destinations.Any())
            {
                context.Destinations.AddRange(new List<Destination>()
                {
                    new Destination
                    {
                        Id = "1",
                        Name = "Mars",
                        Description = "The red planet",
                        Distance = 44000,
                        Price = 9000000,
                        Url =@"/img/tickets/mars.jpg"
                    },
                    new Destination
                    {
                        Id = "2",
                        Name = "Moon",
                        Description = "Our Beloved moon",
                        Distance = 44000,
                        Price = 90000,
                        Url = @"/img/tickets/moon.jpg",
                    },
                    new Destination
                    {
                        Id = "3",
                        Name = "Enceladus",
                        Description = "Saturns cold moon",
                        Distance = 44000,
                        Price = 9000000,
                        Url = @"/img/tickets/enceladus.jpg"

                    }
                });
                context.SaveChanges();
            }

            if (context.Vehicles.Any()) return;
            context.Vehicles.AddRange(new List<Vehicle>()
            {
                new Vehicle
                {
                    Id = "1",
                    Name = "Starship Heavy",
                    Speed = 4560,
                    PilotId = "1",
                    SerialNumber = "SN15",
                    Seats = SeedSeats(seatsVehicleOne),
                    Departure = DateTime.Parse("06/29/2022 05:50:06"),
                    Arrival = DateTime.Parse("12/29/2022 05:50:06"),
                    DestinationId = "1",
                },
                new Vehicle
                {
                    Id = "2",
                    Name = "Dragon",
                    Speed = 3500,
                    PilotId = "3",
                    SerialNumber = "S2316",
                    Seats =SeedSeats(seatsVehicleTwo),
                    Departure = DateTime.Parse("07/15/2022 05:50:06"),
                    Arrival = DateTime.Parse("08/29/2022 05:50:06"),
                    DestinationId = "2"
                }
            });
            context.SaveChanges();
        }
    }
}