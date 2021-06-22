using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Repository.Context;
using Sample.Core.Entities.Models;
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
        public List<ExampleModel> Get()
        {
            _logger.LogDebug("GetAll");
            try
            {
                var result = _context.Set<ExampleModel>()
                    .AsNoTracking()
                    .ToList();

                _logger.LogDebug($"GetAll Result: {result.Count}");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public ExampleModel Get(long Id)
        {
            _logger.LogDebug("Get By Id");
            try
            {
                var result = _context.Set<ExampleModel>()
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == Id);

                _logger.LogDebug($"Get By Id Result: {result?.Id}");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Get By Id Error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<ExampleModel> entities)
        {
            _logger.LogDebug("Create");
            try
            {
                _context.Set<ExampleModel>().AddRange(entities);

                var result = _context.SaveChanges();

                if (result < 1)
                {
                    return 0;
                }
                _logger.LogDebug($"Create: {result} entities created");

                return result;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Create Error: {ex.Message}");
                throw;
            }
        }

        public int Delete(long Id)
        {
            _logger.LogDebug("Delete");
            try
            {
                _context.Set<ExampleModel>().Remove(Get(Id));
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

        public ExampleModel Modify(ExampleModel entity)
        {
            _logger.LogDebug("Modify");
            try
            {
                var actualEntity = Get(entity.Id);
                actualEntity.Name = entity.Name;
                actualEntity.Description = entity.Description;

                _context.Set<ExampleModel>().Update(actualEntity);
                var result = _context.SaveChanges();

                _logger.LogDebug($"Modify: {result} entity modified");

                return result == 1
                  ? actualEntity
                  : new ExampleModel();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, $"Modify Error: {ex.Message}");
                throw;
            }
        }

    }
}
