using AutoMapper;
using Sample.API.AutoMapper.Mappers;

namespace Sample.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ExampleMapper.Map(this);
        }
    }
}