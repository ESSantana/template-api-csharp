using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using Sample.Core.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.Repository.Context
{
    public class SampleDbContext : DbContext
    {
        public readonly DbOptions _dbOptions;
        public readonly ILogger _logger;

        public DbSet<ExampleEntity> ExampleEntities { get; set; }

        public SampleDbContext(DbContextOptions<SampleDbContext> options, IOptionsMonitor<DbOptions> dbOptions, ILogger logger) : base(options)
        {
            _dbOptions = dbOptions.CurrentValue;
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_dbOptions.DefaultScheme);

            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace)
                    && t.GetInterfaces().Any(i => i.Name == typeof(IEntityTypeConfiguration<>).Name
                    && i.Namespace == typeof(IEntityTypeConfiguration<>).Namespace))
                .Select(type => type).ToList();

            types.ForEach(type =>
            {
                ConstructorInfo constructorObj = type.GetConstructor(new Type[] { typeof(DbOptions) });
                if (constructorObj != null)
                {
                    object[] args = new object[] { _dbOptions };
                    dynamic configurationInstance = constructorObj.Invoke(args);
                    modelBuilder.ApplyConfiguration(configurationInstance);
                }
                else
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    modelBuilder.ApplyConfiguration(configurationInstance);
                }

            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                _logger.Debug($"Saved successful!");
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Message: {ex.Message}");
                throw;
            }

        }
    }

}
