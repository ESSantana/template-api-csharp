using Sample.Core.Entities.Models;
using System.Collections.Generic;

namespace Sample.Test.Configuration.Repository
{
    public static class ExampleMockResult
    {
        public static List<ExampleModel> Get()
        {
            return new List<ExampleModel>
            {
                new ExampleModel
                {
                    Id = 1,
                    Name = "Example Mock Name 1",
                    Description = "Example Mock Description 1"
                },
                new ExampleModel
                {
                    Id = 2,
                    Name = "Example Mock Name 2",
                    Description = "Example Mock Description 2"
                },
                new ExampleModel
                {
                    Id = 3,
                    Name = "Example Mock Name 3",
                    Description = "Example Mock Description 3"
                }
            };
        }
    }
}
