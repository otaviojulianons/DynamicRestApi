using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain;
using Repository;
using Services;
using Repository.Repositories;
using Repository.Contexts;

namespace Ioc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>();
            
            services.AddScoped(typeof(ContextRepository<>));
            services.AddScoped(typeof(DynamicDbContext<>));
            services.AddScoped(typeof(DynamicRepository<>));
            services.AddScoped<EntityService>();

            services.AddSingleton<DynamicService>();
            services.AddSingleton<DynamicRoutesCollection>();
        }


    }
}
