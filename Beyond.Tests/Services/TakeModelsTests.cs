using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Beyond.Data.Models;
using Beyond.Models.Control;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Models.DTOs.Output;
using Beyond.Models.Ticket;
using Beyond.Services;
using Beyond.Tests.Mocks;
using Xunit;

namespace Beyond.Tests.Services
{
    public class TakeModelsTests
    {

        [Fact]
        public void ControlPilotsOrNullShouldReturnCollectionOfViewModelsIfThereAreEntities()
        {
            using var db = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            const int range = 3;
            var pilots = Generate<Pilot>(range);
            db.Pilots.AddRange(Enumerable.Range(0, range).Select(_ => new Pilot()).ToList());
            db.SaveChanges();
            var service = new TakeModels(db, null, null, null, mapper);

            var result = service.ControlPilotsOrNull();
            Assert.NotNull(result);
            Assert.Equal(pilots.Count, result.Count);
            Assert.IsType<List<ControlPilotsViewModel>>(result);
        }

        [Fact]
        public void ControlPilotsOrNullShouldReturnNullIfThereAreNoEntities()
        {
            using var db = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var service = new TakeModels(db, null, null, null, mapper);

            var result = service.ControlPilotsOrNull();
            Assert.Null(result);
        }

        [Fact]
        public void AvailablePilotsOrNullShouldReturnPilotsWithNoVehicleOrProvidedPilot()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);
            const int range = 3;
            var expected = range + 1;
            var pilots = Generate<Pilot>(range);
            db.Pilots.AddRange(pilots);
            db.Pilots.Add(new Pilot()
            {
                Vehicle = new Vehicle()
            });
            db.SaveChanges();
            var pilot = db.Pilots.FirstOrDefault(x => x.Vehicle != null);

            var result = service.AvailablePilotsOrNull(pilot.Id);

            Assert.NotNull(result);
            Assert.IsType<List<ControlPilotsViewModel>>(result);
            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void AvailablePilotsOrNullShouldReturnNullIfThereAreNoAvailablePilots()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);
            const int range = 3;
            db.Pilots.AddRange(Enumerable.Range(0, range).Select(_ =>
                new Pilot()
                {
                    Vehicle = new Vehicle()
                }).ToList());
            db.SaveChanges();

            var result = service.AvailablePilotsOrNull("Not existing Id");

            Assert.Null(result);
        }

        [Fact]
        public void VehiclesForEditOrNullShouldReturnNullIfThereAreNoVehicles()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);

            var result = service.VehiclesForEditOrNull();

            Assert.Null(result);
        }

        [Fact]
        public void VehiclesForEditOrNullShouldReturnAllTheVehicles()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);
            db.Vehicles.AddRange(Enumerable.Range(0, 4).Select(_ =>
                 new Vehicle()
                 {
                     Pilot = new Pilot(),
                     Destination = new Destination()
                 }).ToList());
            db.SaveChanges();

            var result = service.VehiclesForEditOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<EditVehicleViewModel>>(result);
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void DestinationsForEditOrNullShouldReturnNullIfThereAreNoDestinations()
        {
            using var db = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var service = new TakeModels(db, null, null, null, mapper);

            var result = service.DestinationsForEditOrNull();

            Assert.Null(result);
        }

        [Fact]
        public void DestinationsForEditOrNullShouldReturnAllDestinations()
        {
            using var db = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var service = new TakeModels(db, null, null, null, mapper);
            const int range = 5;
            var destinations = Generate<Destination>(range);
            db.Destinations.AddRange(destinations);
            db.SaveChanges();

            var result = service.DestinationsForEditOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<EditDestinationViewModel>>(result);
            Assert.Equal(range, result.Count);
        }

        [Fact]
        public void PilotsForEditOrNullShouldReturnNullIfThereAreNoPilots()
        {
            using var db = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var service = new TakeModels(db, null, null, null, mapper);

            var result = service.PilotsForEditOrNull();

            Assert.Null(result);
        }

        [Fact]
        public void PilotsForEditOrNullShouldReturnAllThePilots()
        {
            using var db = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var service = new TakeModels(db, null, null, null, mapper);
            const int range = 5;
            var pilots = Generate<Pilot>(range);
            db.Pilots.AddRange(pilots);
            db.SaveChanges();
            var result = service.PilotsForEditOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<EditPilotViewModel>>(result);
            Assert.Equal(pilots.Count, result.Count);
        }

        [Fact]
        public void ControlDestinationsOrNullShouldReturnNullIfThereAreNoDestinations()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);

            var result = service.ControlDestinationsOrNull();

            Assert.Null(result);
        }

        [Fact]
        public void ControlDestinationsOrNullShouldReturnAllDestinations()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);
            const int range = 5;
            var destinations = Generate<Destination>(range);
            db.Destinations.AddRange(destinations);
            db.SaveChanges();

            var result = service.ControlDestinationsOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<ControlDestinationsViewModel>>(result);
            Assert.Equal(destinations.Count, result.Count);
        }

        [Fact]
        public void CrewOrNullShouldReturnAllPilotsIfThereAreAny()
        {
            using var db = DatabaseMock.Instance;
            var enumNames = new TakeRanks();
            var service = new TakeModels(db, enumNames, null, null, null);
            const int range = 5;
            var pilots = Generate<Pilot>(range);
            db.Pilots.AddRange(pilots);
            db.SaveChanges();

            var result = service.CrewOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<CrewViewModel>>(result);
            Assert.Equal(pilots.Count, result.Count);
        }

        [Fact]
        public void CrewOrNullShouldReturnNullIfThereAreNoPilots()
        {
            using var db = DatabaseMock.Instance;
            var enumNames = new TakeRanks();
            var service = new TakeModels(db, enumNames, null, null, null);

            var result = service.CrewOrNull();

            Assert.Null(result);
        }

        [Fact]
        public void DestinationsOrNullShouldReturnAllDestinations()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);
            const int range = 4;
            var destinations = Generate<Destination>(range);
            db.Destinations.AddRange(destinations);
            db.SaveChanges();

            var result = service.DestinationsOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<DestinationViewModel>>(result);
            Assert.Equal(destinations.Count, result.Count);

        }

        [Fact]
        public void DestinationsOrNullShouldReturnNullIfThereAreNoDestinations()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);

            var result = service.DestinationsOrNull();

            Assert.Null(result);
        }

        [Fact]
        public void TicketsOrNullShouldReturnAllTheTickets()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);
            const int range = 7;
            var vehicles = Enumerable.Range(0, range).Select(x => new Vehicle()
            {
                Destination = new Destination(),
                Pilot = new Pilot()
            }).ToList();
               
            db.Vehicles.AddRange(vehicles);
            db.SaveChanges();

            var result = service.TicketsOrNull();

            Assert.NotNull(result);
            Assert.IsType<List<TicketViewModel>>(result);
            Assert.Equal(vehicles.Count, result.Count);
        }

        [Fact]
        public void TicketsOrNullShouldReturnNullIfThereAreNoVehicles()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeModels(db, null, null, null, null);

            var result= service.TicketsOrNull();

            Assert.Null(result);
        }

        private static List<T> Generate<T>(int count) => Enumerable.Range(0, count).Select(_ => (T)Activator.CreateInstance(typeof(T))).ToList();
    }
}