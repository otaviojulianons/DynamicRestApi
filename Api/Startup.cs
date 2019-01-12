using Api.Middlewares;
using Application.EventHandlers;
using Application.Services;
using Ioc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
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
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DynamicRestApi", Version = "v1" });
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

            services.AddCors(o => o.AddPolicy("CorsConfig", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMediatR(Assembly.GetAssembly(typeof(DynamicEventHandler)));
            services.UseWebSocketService();


            IoCService.Configure(services, _configuration);
            Application.AutoMapper.MapperRegister();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceScopeFactory serviceScopeFactory)
        {

            app.UseStaticFiles();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            //app.UseObservableControllerMiddlewareDynamic();
            app.UseMiddleware<WebSocketStartMiddleware>();
            app.UseMiddleware<WebSocketUpdateMiddleware>();
            app.UseMiddleware<DynamicRoutesMiddleware>();
           

            app.UseCors("CorsConfig");
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseMvc();

            await StartAppService.Run(serviceScopeFactory);
        }

    }
}
