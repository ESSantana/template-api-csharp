using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Core.Entities;
using Sample.Repository.Context;
using Sample.Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Sample.Repository.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly SampleDbContext _context;
        private readonly ILogger<ExampleRepository> _logger;

        public ExampleRepository(SampleDbContext context, ILogger<ExampleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ExampleEntity> Get()
        {
            _logger.LogDebug("GetAll");
            try
            {
                var result = _context.ExampleEntities.
                    AsNoTracking()
                    .ToList();

                _logger.LogDebug($"Get Result: {result.Count}");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public ExampleEntity Get(long Id)
        {
            _logger.LogDebug("Get");
            try
            {
                var result = _context.ExampleEntities.
                    AsNoTracking()
                    .FirstOrDefault(x => x.Id == Id);

                _logger.LogDebug($"Get Result: {result?.Id}");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Get Error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<ExampleEntity> entities)
        {
            _logger.LogDebug("Create");
            try
            {
                _context.ExampleEntities.AddRange(entities);

                var result = _context.SaveChanges();

                if (result > 0)
                {
                    _logger.LogDebug($"Create: {result} entities created");

                    return result;
                }
                return 0;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public int Delete(long Id)
        {
            _logger.LogDebug("Delete");
            try
            {
                _context.ExampleEntities.Remove(Get(Id));
                var result = _context.SaveChanges();

                _logger.LogDebug($"Delete: entity with id({Id}) deleted");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Delete Error: {ex.Message}");
                throw;
            }
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {
            _logger.LogDebug("Modify");
            try
            {
                var actualEntity = Get(entity.Id);
                actualEntity.Name = entity.Name;
                actualEntity.Description = entity.Description;

                _context.ExampleEntities.Update(actualEntity);
                var result = _context.SaveChanges();

                _logger.LogDebug($"Modify: {result} entity modified");

                return result == 1
                  ? actualEntity
                  : new ExampleEntity();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Modify Error: {ex.Message}");
                throw;
            }
        }
    }
}
