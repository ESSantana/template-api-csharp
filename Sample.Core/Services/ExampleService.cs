using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using Sample.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;

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
        public ExampleEntity Get(Guid Id)
        {
            return _repository.Get(Id);
        }

        public ExampleEntity Create(ExampleEntity entity)
        {
            return _repository.Create(entity);
        }

        public ExampleEntity Delete(Guid Id)
        {
            return _repository.Delete(Id);
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {
            return _repository.Modify(entity);
        }
    }
}
