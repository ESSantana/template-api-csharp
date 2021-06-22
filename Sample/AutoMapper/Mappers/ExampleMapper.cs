using AutoMapper;
using Sample.API.DTO;
using Sample.Core.Entities;
using Sample.Core.Entities.Models;


namespace Sample.API.AutoMapper.Mappers
{
    public static class ExampleMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<ExampleDTO, ExampleEntity>();
            profile.CreateMap<ExampleEntity, ExampleDTO>();
            profile.CreateMap<ExampleModel, ExampleEntity>();
            profile.CreateMap<ExampleEntity, ExampleModel>();
        }
    }
}
