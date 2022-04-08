using System;
using System.Linq;
using System.Collections.Generic;

using Beyond.Data.Models;
using Beyond.Data.Models.Enums;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Beyond.Data
{
    public class AppDbInitializer
    {
        public static async void CreateAdmin(IApplicationBuilder applicationBuilder)
        {
            using var services = applicationBuilder.ApplicationServices.CreateScope();
            var userManager = services.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = services.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            if (await userManager.FindByEmailAsync("admin@admin.com") != null) return;
            var user = new User { UserName = "admin@admin.com", Email = "admin@admin.com" };
            var result = await userManager.CreateAsync(user, "admin123456");
            if (result.Errors.Any())
            {
                throw new ApplicationException("Could not generate admin");
            }

            var role = new IdentityRole()
            {
                Name = "Admin"
            };
            var admin = await userManager.FindByEmailAsync("admin@admin.com");
            await roleManager.CreateAsync(role);
            await userManager.AddToRoleAsync(admin, "Admin");


        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var generate = serviceScope.ServiceProvider.GetService<IGenerate>();
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
                        Distance = 54000000,
                        Price = 900,
                        Url =@"https://i.pinimg.com/564x/c9/d7/62/c9d76291e8969c9a49216015fbe837c8.jpg"
                    },
                    new Destination
                    {
                        Id = "2",
                        Name = "Moon",
                        Description = "Our Beloved moon",
                        Distance = 384400,
                        Price = 150,
                        Url = @"https://i.pinimg.com/564x/5b/32/b9/5b32b958b1034594cc24163c72ab91ba.jpg",
                    },
                    new Destination
                    {
                        Id = "3",
                        Name = "Enceladus",
                        Description = "Saturns cold moon .. out of reach ;s",
                        Distance = 123000000,
                        Price = 900,
                        Url = @"https://i.pinimg.com/564x/22/b4/24/22b424ee410cb9aeb38291cf8075963f.jpg"

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
                    Speed = 27000,
                    PilotId = "1",
                    SerialNumber = "SN15",
                    Seats = generate.Seats(seatsVehicleTwo),
                    Departure = DateTime.Parse("06/29/2022 05:50:06"),
                    LaunchSite = "Mariopul",
                    Arrival = DateTime.Parse("06/29/2022 05:50:06").AddMonths(9),
                    DestinationId = "1",
                },
                new Vehicle
                {
                    Id = "2",
                    Name = "Dragon",
                    Speed = 27360,
                    PilotId = "3",
                    SerialNumber = "S2316",
                    LaunchSite = "Sofia",
                    Seats =generate.Seats(seatsVehicleTwo),
                    Departure = DateTime.Parse("07/15/2022 05:50:06"),
                    Arrival = DateTime.Parse("07/15/2022 05:50:06").AddDays(3),
                    DestinationId = "2"
                }
            });
            context.SaveChanges();
            var pilot1 = context.Pilots.First(p => p.Id == "1");
            pilot1.VehicleId = "1";
            context.Pilots.Update(pilot1);
            context.SaveChanges();

        }

    }
}