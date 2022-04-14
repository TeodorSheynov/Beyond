using AutoMapper;

namespace Beyond.Tests.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapConfig = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });
                return new Mapper(mapConfig);
            }
        }
    }
}