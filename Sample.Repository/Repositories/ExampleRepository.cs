using Sample.Core.Entities;
using Sample.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Sample.Repository.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        public ExampleRepository()
        {

        }
        public List<ExampleEntity> Get()
        {
            return new List<ExampleEntity>
            {
                new ExampleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Mock Name 1",
                    Description = "Mock Description 1"
                },
                new ExampleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Mock Name 2",
                    Description = "Mock Description 2"
                }
            };
        }

        public ExampleEntity Get(Guid Id)
        {
            return new ExampleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Mock Name",
                Description = "Mock Description"
            };
        }

        public ExampleEntity Create(ExampleEntity entity)
        {
            return new ExampleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Mock Name",
                Description = "Mock Description"
            };
        }

        public ExampleEntity Delete(Guid Id)
        {
            return new ExampleEntity
            {
                Id = Id,
                Name = "Mock Name",
                Description = "Mock Description"
            };
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {
            return entity;
        }
    }
}
