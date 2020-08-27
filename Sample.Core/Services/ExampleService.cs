using Microsoft.EntityFrameworkCore.Internal;
using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using Sample.Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _repository;

        public ExampleService(IExampleRepository repository)
        {
            _repository = repository;
        }

        public List<ExampleEntity> Get()
        {
            return _repository.Get();
        }
        public ExampleEntity Get(long Id)
        {
            if (Id < 1)
            {
                return null;
            }

            return _repository.Get(Id);
        }

        public int Create(List<ExampleEntity> entities)
        {
            if (entities.Any(x => string.IsNullOrEmpty(x.Name)))
            {
                return 0;
            }

            return _repository.Create(entities.Where(x => x.Id == 0).ToList());
        }

        public int Delete(long Id)
        {
            var entityToDelete = Get(Id);
            if (entityToDelete == null)
            {
                return 0;
            }

            return _repository.Delete(Id);
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {

            if (entity.Id > 0)
            {
                var actualEntity = Get(entity.Id);

                if (string.IsNullOrEmpty(actualEntity.Name))
                {
                    return new ExampleEntity();
                }

                return _repository.Modify(entity);
            }

            return null;
        }
    }
}
