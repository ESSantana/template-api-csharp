using AutoMapper;
using Sample.Core.Entities;

public static class ExampleMapper
{
    public static void Map(Profile profile)
    {
        profile.CreateMap<ExampleEntity, ExampleEntity>();
    }
}