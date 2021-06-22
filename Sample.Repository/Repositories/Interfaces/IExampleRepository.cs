using Sample.Core.Entities.Models;
using System.Collections.Generic;

namespace Sample.Repository.Repositories
{
    public interface IExampleRepository
    {
        List<ExampleModel> Get();
        ExampleModel Get(long Id);
        int Create(List<ExampleModel> entity);
        ExampleModel Modify(ExampleModel entity);
        int Delete(long Id);
    }
}
