using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sample.API;
using Sample.Core.Services;
using Sample.Core.Services.Interfaces;
using Sample.Repository.Context;
using Sample.Repository.Repositories;
using Sample.Repository.Repositories.Interfaces;
using System;
using System.Reflection;

namespace Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Example API",
                    Description = "An API example to use as a template",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Name",
                        Email = "example@mail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under ExampleLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            services.AddDbContext<SampleDbContext>(opt => opt.UseMySql("Server=127.0.0.1:3306;Database=sample;Uid=root;Pwd=root;"));

            services.AddTransient<IExampleRepository, ExampleRepository>();
            services.AddTransient<IExampleService, ExampleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Example API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
