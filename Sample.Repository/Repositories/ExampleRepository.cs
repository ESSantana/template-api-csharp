using Microsoft.EntityFrameworkCore;
using Sample.Core.Entities;
using Sample.Repository.Context;
using Sample.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Repository.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        private readonly SampleDbContext _context;
        // private readonly ILogger _logger;

        public ExampleRepository(SampleDbContext context)
        {
            _context = context;
            // _logger = logger;
        }
        public List<ExampleEntity> Get()
        {
            // _logger.Debug("GetAll");
            try
            {
                var result = _context.ExampleEntities.
                    AsNoTracking()
                    .ToList();

                // _logger.Info($"Get Result: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public ExampleEntity Get(long Id)
        {
            // _logger.Debug("Get");
            try
            {
                var result = _context.ExampleEntities.
                    AsNoTracking()
                    .FirstOrDefault(x => x.Id == Id);

                // _logger.Info($"Get Result: {result.Id}");

                return result;
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public int Create(List<ExampleEntity> entities)
        {
            try
            {
                _context.ExampleEntities.AddRange(entities);

                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return result;
                }
                // _logger.Info($"Get Result: {result.Id}");
                return 0;
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public int Delete(long Id)
        {
            try
            {
                _context.ExampleEntities.Remove(Get(Id));
                var result = _context.SaveChanges();

                return result;
                // _logger.Info($"Get Result: {result.Id}");
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {
            try
            {
                var actualEntity = Get(entity.Id);
                actualEntity.Name = entity.Name;
                actualEntity.Description = entity.Description;

                _context.ExampleEntities.Update(actualEntity);
                return _context.SaveChanges() == 1 ? actualEntity : new ExampleEntity();
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }
    }
}
