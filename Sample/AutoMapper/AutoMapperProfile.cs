using AutoMapper;

namespace Sample.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ExampleMapper.Map(this);
        }
    }
}