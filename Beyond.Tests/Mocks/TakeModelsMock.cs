using System.Collections.Generic;
using System.Linq;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Services.Interfaces;
using Moq;

namespace Beyond.Tests.Mocks
{
    public static class TakeModelsMock
    {
        public static ITakeModels CrewOrNullPopulatedInstance
        {
            get
            {
                var takeModel = new Mock<ITakeModels>();
                takeModel.Setup(s => s.CrewOrNull())
                    .Returns(Enumerable.Range(0, 5).Select(c => new CrewViewModel()).ToList);
                return takeModel.Object;
            }
            
        }
        public static ITakeModels CrewOrNullEmptyInstance
        {
            get
            {
                var takeModel = new Mock<ITakeModels>();
                takeModel.Setup(s => s.CrewOrNull())
                    .Returns((List<CrewViewModel>)null);
                return takeModel.Object;
            }

        }

        public static ITakeModels DestinationsOrNullPopulatedInstance
        {
            get
            {
                var takeModel = new Mock<ITakeModels>();
                takeModel.Setup(s => s.DestinationsOrNull())
                    .Returns(Enumerable.Range(0, 5).Select(c => new DestinationViewModel()).ToList);
                return takeModel.Object;
            }
        }

        public static ITakeModels DestinationsOrNullEmptyInstance
        {
            get
            {
                var takeModel= new Mock<ITakeModels>();
                takeModel.Setup(s => s.DestinationsOrNull())
                    .Returns((List<DestinationViewModel>) null);
                return takeModel.Object;
            }
        }
    }
}