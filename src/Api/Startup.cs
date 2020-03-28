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

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMvc(options =>
            {
                //options.Conventions.Add(new DynamicControllerModelConvention());
            })
                //.ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new RemoteControllerFeatureProvider(logger)))
                .AddApplicationPart(typeof(Bootstrap).Assembly);
                //.AddControllersAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DynamicRestApi", Version = "v1" });
                c.ExampleFilters();
                c.DescribeAllEnumsAsStrings(); 
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Api.xml");
                c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("apikey", new ApiKeyScheme
                {
                    Description = "Authorization key to access Dynamic Rest API",
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

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseGraphiQl("/graphql");
            app.UseMvc();

            new StartupService(serviceProvider).Start();
        }

    }
}
