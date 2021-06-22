using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Resources;
using Sample.Repository.Context;
using Sample.Repository.Repositories;
using Sample.Service.Services;

namespace Sample.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureServiceDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<SampleDbContext>();

            services.AddTransient<IResourceLocalizer, ResourceLocalizer>();
            services.AddTransient<IExampleRepository, ExampleRepository>();
            services.AddTransient<IExampleService, ExampleService>();
        }
    }
}
