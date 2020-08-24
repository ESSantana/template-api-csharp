using Sample.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sample.Repository.Repositories.Interfaces
{
    public interface IExampleRepository
    {
        List<ExampleEntity> Get();
        ExampleEntity Get(Guid Id);
        ExampleEntity Create(ExampleEntity entity);
        ExampleEntity Modify(ExampleEntity entity);
        ExampleEntity Delete(Guid Id);
    }
}
