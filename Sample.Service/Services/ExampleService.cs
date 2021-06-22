using AutoMapper;
using Microsoft.Extensions.Logging;
using Sample.Core.Entities;
using Sample.Core.Entities.Models;
using Sample.Repository.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Service.Services
{
    public class ExampleService : IExampleService
    {
        private readonly ILogger<ExampleService> _logger;
        private readonly IMapper _mapper;
        private readonly IExampleRepository _repository;

        public ExampleService(IExampleRepository repository, ILogger<ExampleService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<ExampleEntity> Get()
        {
            _logger.LogDebug("Get All");
            var result = _repository.Get();

            _logger.LogDebug($"Get All Result: {result.Count} entities");
            return result.Select(r => _mapper.Map<ExampleEntity>(r)).ToList();
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
            return _mapper.Map<ExampleEntity>(result);
        }

        public int Create(List<ExampleEntity> entities)
        {
            _logger.LogDebug("Create");

            if (entities.Any(x => string.IsNullOrEmpty(x.Name)))
            {
                _logger.LogWarning("Missing required property 'Name'");
                return 0;
            }

            var filteredEntities = entities.Where(x => x.Id == 0).ToList();
            var entitiesToCreate = filteredEntities.Select(e => _mapper.Map<ExampleModel>(e)).ToList();

            var result = _repository.Create(entitiesToCreate);

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
            if (entity.Id < 1)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var actualEntity = Get(entity.Id);

            if (actualEntity == null)
            {
                _logger.LogWarning("Invalid ID");
                return null;
            }

            var result = _repository.Modify(_mapper.Map<ExampleModel>(entity));
            _logger.LogDebug($"Modify success? {!string.IsNullOrEmpty(result.Name)}");

            return _mapper.Map<ExampleEntity>(result);
        }
    }
}
