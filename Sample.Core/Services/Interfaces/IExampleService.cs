using Sample.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sample.Core.Services.Interfaces
{
    public interface IExampleService
    {
        List<ExampleEntity> Get();
        ExampleEntity Get(Guid Id);
        ExampleEntity Create(ExampleEntity entity);
        ExampleEntity Modify(ExampleEntity entity);
        ExampleEntity Delete(Guid Id);
    }
}
