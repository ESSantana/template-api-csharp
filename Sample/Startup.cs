using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Sample.API;
using Sample.API.Filters;
using Sample.Authorization;
using Sample.Core.Resources;
using Sample.Core.Services;
using Sample.Core.Services.Interfaces;
using Sample.Repository.Context;
using Sample.Repository.Repositories;
using Sample.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            services.AddMvc()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssembly(Assembly.Load("Sample.API")))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(Resource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("Resource", assemblyName.Name);
                    };
                });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(ExceptionFilter));
            });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("pt-BR"),
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                });

            services.AddControllers();
            services.AddCors();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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

            services.AddHealthChecks()
                .AddCheck<CustomHealthCheck>("applicationHealth", tags: new[] { "app_tag" });

            services.AddDbContext<SampleDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DevConnection")));
            services.AddScoped<SampleDbContext>();

            services.AddTransient<IResourceLocalizer, ResourceLocalizer>();
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

            var supportedCultures = new[] { new CultureInfo("pt-BR") };

            var localization = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localization.Value);

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseHealthChecks("/healthy", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("app_tag"),
                ResponseWriter = WriteResponse
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Example API");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async static Task WriteResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var result = JsonConvert.SerializeObject(
                new
                {
                    statusApplication = report.Status.ToString(),
                    healthChecks = report.Entries.Select(e => new
                    {
                        check = e.Key,
                        ErrorMessage = e.Value.Exception?.Message,
                        statusMessage = e.Value.Description,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                    })
                });

            await context.Response.WriteAsync(result);
        }
    }
}
