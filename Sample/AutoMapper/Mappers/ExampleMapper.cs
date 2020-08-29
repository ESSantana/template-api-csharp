using AutoMapper;
using Sample.API.Models.DTO;
using Sample.Core.Entities;

public static class ExampleMapper
{
    public static void Map(Profile profile)
    {
        profile.CreateMap<ExampleDTO, ExampleEntity>();
        profile.CreateMap<ExampleEntity, ExampleDTO>();
    }
}