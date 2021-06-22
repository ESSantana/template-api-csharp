using AutoMapper;
using Sample.API.AutoMapper;

namespace Sample.Test.Configuration.Factory
{
    public static class AutoMapperFactory
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
