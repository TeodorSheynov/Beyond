using System;
using Beyond.Data.Models;
using Beyond.Services;
using Beyond.Tests.Mocks;
using Xunit;

namespace Beyond.Tests.Services
{
    public class TakeEntityByIdTests
    {
        [Fact]
        public void DestinationShouldReturnTheDestinationCorrespondingToTheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "4";
            db.Destinations.Add(new Destination()
            {
                Id=id,
            });
            db.SaveChanges();

            var result = service.Destination(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.IsType<Destination>(result);
        }

        [Fact]
        public void DestinationShouldThrowExceptionIfThereIsNoDestinationsWIthTheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "4";

            Assert.Throws<ArgumentNullException>(() => service.Destination(id));
        }

        [Fact]
        public void PilotShouldReturnThePilotCorrespondingToTheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "2";
            db.Pilots.Add(new Pilot()
            {
                Id = id,
            });
            db.SaveChanges();

            var result=service.Pilot(id);

            Assert.NotNull(result);
            Assert.IsType<Pilot>(result);
            Assert.Equal(id,result.Id);
        }

        [Fact]
        public void PilotShouldThrowExceptionIfThereIsNoPilotToTheCorrespondingId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "4";
            db.Pilots.Add(new Pilot());
            db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() => service.Pilot(id));
        }
        [Fact]
        public void UserShouldReturnUserCorrespondingToRheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "4";
            db.Users.Add(new User()
            {
                Id = id
            });
            db.SaveChanges();

            var result = service.User(id);

            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(id,result.Id);
        }

        [Fact]
        public void UserShouldThrowExceptionIfThereIsNoUserWithTHeGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "5";

            Assert.Throws<ArgumentNullException>(() => service.User(id));
        }

        [Fact]
        public void VehicleShouldReturnTheVehicleWithTheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "2";
            db.Vehicles.Add(new Vehicle()
            {
                Id = id
            });
            db.SaveChanges();

            var result=service.Vehicle(id);

            Assert.NotNull(result);
            Assert.IsType<Vehicle>(result);
            Assert.Equal(id,result.Id);
        }

        [Fact]
        public void VehicleShouldThrowExceptionIfThereIsNoVehicleWithSuchId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "2";
            const string searchId = "6";
            db.Vehicles.Add(new Vehicle()
            {
                Id = id
            });
            db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() => service.Vehicle(searchId));
        }

        [Fact]
        public void TicketShouldReturnTheTicketWithTheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "2";
            db.Tickets.Add(new Ticket()
            {
                Id = id,
                Vehicle = new Vehicle()
            });
            db.SaveChanges();

            var result = service.Ticket(id);

            Assert.NotNull(result);
            Assert.IsType<Ticket>(result);
            Assert.Equal(id,result.Id);
        }

        [Fact]
        public void TicketShouldThrowExceptionIfThereIsNoTicketWithTheGivenId()
        {
            using var db = DatabaseMock.Instance;
            var service = new TakeEntityById(db);
            const string id = "2";
            const string searchId= "6";
            db.Tickets.Add(new Ticket()
            {
                Id = id,
                Vehicle = new Vehicle()
            });

            Assert.Throws<ArgumentNullException>(() => service.Ticket(searchId));
        }
    }
}