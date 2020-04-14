using Api.Examples;
using Application.Services;
using GraphiQl;
using Infrastructure.DI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Api
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        private readonly string _apiVersion;
        private readonly string _apiName;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiName = configuration.GetValue<string>("Api:Name");
            _apiVersion = configuration.GetValue<string>("Api:Version");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddApplicationPart(typeof(Bootstrap).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = _apiName, Version = "v" + _apiVersion });
                c.ExampleFilters();
                c.DescribeAllEnumsAsStrings(); 
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Api.xml");
                c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("apikey", new ApiKeyScheme
                {
                    Description = $"Authorization key to access {_apiName}",
                    Name = "ApiKey",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"apikey", new string[] { } }
                });

            });
            services.AddSwaggerExamplesFromAssemblyOf<EntityExample>();

            services.AddCors(o => o.AddPolicy("CorsConfig", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMediatR(Assembly.GetAssembly(typeof(StartupService)));

            Bootstrap.Run(services);
            Application.AutoMapper.Register();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsConfig");
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseGraphiQl("/graphql");
            app.UseMvc();

            new StartupService(serviceProvider).Start();
        }

    }
}
