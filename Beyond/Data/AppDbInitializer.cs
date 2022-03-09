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
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope=applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Crews.Any())
            {
                context.Crews.AddRange(new List<Crew>()
                {
                    new Crew()
                    {
                        Id="1",
                        Age = 34,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot1.jpg",
                        Name = "Colby Smith",
                        Rank = Rank.Pilot
                    },
                    new Crew()
                    {   Id = "2",
                        Age = 43,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot2.jpg",
                        Name = "John Smith",
                        Rank = Rank.Assistant
                    },
                    new Crew()
                    {
                        Id = "3",
                        Age = 44,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot3.jpg",
                        Name = "Satoshi Nakamoto",
                        Rank = Rank.Assistant
                    },
                    new Crew()
                    {
                        Id = "4",
                        Age = 34,
                        Description = "Some Description",
                        ImgPath = @"/img/crew/pilot4.jpg",
                        Name = "Lacy Colt",
                        Rank = Rank.Assistant
                    }
                });
                context.SaveChanges();
            }
            if (!context.Destinations.Any())
            {
                context.Destinations.AddRange(new List<Destination>()
                {
                    new Destination()
                    {
                        Id = "1",
                        Name = "Mars",
                        Description = "The red planet",
                        Distance = 44000,
                        Price = 9000000
                    },
                    new Destination()
                    {
                        Id = "2",
                        Name = "Moon",
                        Description = "Our Beloved moon",
                        Distance = 44000,
                        Price = 90000
                    },
                    new Destination
                    {
                        Id = "3",
                        Name = "Enceladus",
                        Description = "Saturns cold moon",
                        Distance = 44000,
                        Price = 9000000,

                    }
                });
                context.SaveChanges();
            }
            if (!context.Vehicles.Any())
            {
                context.Vehicles.AddRange(new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        Id = "1",
                        Name = "Starship Heavy",
                        Description = "A big ship",
                        Seats = 12
                    },
                    new Vehicle()
                    {
                        Id="2",
                        Name = "Dragon",
                        Description = "A small ship",
                        Seats = 6
                    }
                });
                context.SaveChanges();
            }
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(new List<Ticket>()
                {
                    new Ticket
                    {
                        Id = "1",
                        DestinationId = "1",
                        Description = "Trip to Mars",
                        VehicleId = "1",
                        ImgPath = @"/img/tickets/mars.jpg"
                    },
                    new Ticket
                    {
                        Id = "2",
                        DestinationId = "2",
                        Description = "Astonishing view from the Moon",
                        VehicleId = "2",
                        ImgPath = @"/img/tickets/moon.jpg"
                    },
                    new Ticket
                    {
                        Id = "3",
                        DestinationId = "3",
                        Description = "The cold moon of Saturn",
                        VehicleId = "1",
                        ImgPath = @"/img/tickets/enceladus.jpg"
                    }
                });
                context.SaveChanges();
            }
        }
    }
}