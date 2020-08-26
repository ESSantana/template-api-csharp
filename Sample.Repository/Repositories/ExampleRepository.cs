using Microsoft.EntityFrameworkCore;
using NLog;
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
        private readonly ILogger _logger;

        public ExampleRepository(SampleDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ExampleEntity> Get()
        {
            _logger.Debug("GetAll");
            try
            {
                var result = _context.ExampleEntities.
                    AsNoTracking()
                    .ToList();

                _logger.Info($"Get Result: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetAll Error: {ex.Message}");
                throw;
            }
        }

        public ExampleEntity Get(Guid Id)
        {
            return new ExampleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Mock Name",
                Description = "Mock Description"
            };
        }

        public ExampleEntity Create(ExampleEntity entity)
        {
            return new ExampleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Mock Name",
                Description = "Mock Description"
            };
        }

        public ExampleEntity Delete(Guid Id)
        {
            return new ExampleEntity
            {
                Id = Id,
                Name = "Mock Name",
                Description = "Mock Description"
            };
        }

        public ExampleEntity Modify(ExampleEntity entity)
        {
            return entity;
        }
    }
}
