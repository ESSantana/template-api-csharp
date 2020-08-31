using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using Sample.Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Services
{
    public class ExampleService : IExampleService
    {
        private readonly ILogger<ExampleService> _logger;
        private readonly IExampleRepository _repository;

        public ExampleService(IExampleRepository repository, ILogger<ExampleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public List<ExampleEntity> Get()
        {
            _logger.LogDebug("Get All");
            var result = _repository.Get();

            _logger.LogDebug($"Get All Result: {result.Count} entities");
            return result;
        }

        public ExampleEntity Get(long Id)
        {
            _logger.LogDebug("Get");
            if (Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var result = _repository.Get(Id);

            _logger.LogDebug($"Get result? {result != null}");
            return result;
        }

        public int Create(List<ExampleEntity> entities)
        {
            _logger.LogDebug("Create");

            if (entities.Any(x => string.IsNullOrEmpty(x.Name)))
            {
                _logger.LogWarning("Missing required property 'Name'");
                return 0;
            }

            var result = _repository.Create(entities.Where(x => x.Id == 0).ToList());

            _logger.LogDebug($"Create: {result} entities created");

            return result;
        }

        public int Delete(long Id)
        {
            _logger.LogDebug("Delete");
            var entityToDelete = Get(Id);

            if (entityToDelete == null)
            {
                _logger.LogWarning("Invalid ID");
                return 0;
            }

            var result = _repository.Delete(Id);
            _logger.LogDebug($"Delete: entity with id({Id}) deleted");

            return result;
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {
            _logger.LogDebug("Modify");
            if (entity.Id > 0)
            {
                var actualEntity = Get(entity.Id);

                if (actualEntity == null)
                {
                    _logger.LogWarning("Invalid ID");
                    return null;
                }

                var result = _repository.Modify(entity);
                _logger.LogDebug($"Modify success? {!string.IsNullOrEmpty(result.Name)}");

                return result;
            }

            _logger.LogWarning("Invalid ID");
            return null;
        }
    }
}
