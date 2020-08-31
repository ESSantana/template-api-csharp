using Sample.Core.Entities;
using System.Collections.Generic;

namespace Sample.Test.Configuration.Service
{
    public static class ExampleMockResult
    {
        public static List<ExampleEntity> Get()
        {
            return new List<ExampleEntity>
            {
                new ExampleEntity
                {
                    Id = 1,
                    Name = "Example Mock Name 1",
                    Description = "Example Mock Description 1"
                },
                new ExampleEntity
                {
                    Id = 2,
                    Name = "Example Mock Name 2",
                    Description = "Example Mock Description 2"
                },
                new ExampleEntity
                {
                    Id = 3,
                    Name = "Example Mock Name 3",
                    Description = "Example Mock Description 3"
                }
            };
        }
    }
}
