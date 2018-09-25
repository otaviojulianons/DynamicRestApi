using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain;
using Repository;
using Services;
using Repository.Repositories;
using Repository.Contexts;
using Domain.Helpers.Collections;
using Domain.Interfaces;
using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
using Domain.Entities.LanguageAggregate;
using Domain.Services;

namespace Ioc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //REPOSITORY
            services.AddDbContext<AppDbContext>();
            services.AddScoped(typeof(ContextRepository<>));
            services.AddScoped(typeof(DynamicDbContext<>));
            services.AddScoped(typeof(DynamicRepository<>));

            services.AddScoped<IDatabaseService,AppDbContext>();
            services.AddScoped<IRepository<EntityDomain>, ContextRepository<EntityDomain>>();
            services.AddScoped<IRepository<AttributeDomain>, ContextRepository<AttributeDomain>>();
            services.AddScoped<IRepository<DataTypeDomain>, ContextRepository<DataTypeDomain>>();
            services.AddScoped<IRepository<LanguageDomain>, ContextRepository<LanguageDomain>>();
            services.AddScoped<IRepository<LanguageDataTypeDomain>, ContextRepository<LanguageDataTypeDomain>>();

            //SERVICES
            services.AddSingleton<IDynamicService,DynamicService>();
            services.AddSingleton<DynamicRoutesCollection>();
            services.AddScoped<DataTypeService>();
            services.AddScoped<LanguageService>();
            services.AddScoped<EntityService>();
        }


    }
}
