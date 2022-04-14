using System.Collections.Generic;
using System.Linq;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Models.Control;
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
            const int expected = 3;
            db.Pilots.AddRange(Enumerable.Range(0,expected).Select(x=>new Pilot()).ToList());
            db.SaveChanges();
            var service = new TakeModels(db,null,null,null,mapper);

            var result = service.ControlPilotsOrNull();
            Assert.NotNull(result);
            Assert.Equal(expected,result.Count());
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
    }
}