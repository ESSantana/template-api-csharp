using Sample.Core.Entities;
using System.Collections.Generic;

namespace Sample.Service.Services
{
    public interface IExampleService
    {
        List<ExampleEntity> Get();
        ExampleEntity Get(long Id);
        int Create(List<ExampleEntity> entities);
        ExampleEntity Modify(ExampleEntity entity);
        int Delete(long Id);
    }
}
