using Sample.Core.Entities;
using System.Collections.Generic;

namespace Sample.Repository.Repositories.Interfaces
{
    public interface IExampleRepository
    {
        List<ExampleEntity> Get();
        ExampleEntity Get(long Id);
        int Create(List<ExampleEntity> entity);
        ExampleEntity Modify(ExampleEntity entity);
        int Delete(long Id);
    }
}
