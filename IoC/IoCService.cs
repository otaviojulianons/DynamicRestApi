using Application.Services;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Repository.Contexts;
using Infrastructure.Repository.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Messaging;

namespace Ioc
{
    public class IoCService
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            //REPOSITORY
            services.AddDbContext<AppDbContext>();
            services.AddScoped(typeof(ContextRepository<>));
            services.AddScoped(typeof(DynamicDbContext<>));
            services.AddScoped(typeof(DynamicRepository<>));
            services.AddScoped<IDatabaseService,AppDbContext>();
            services.AddScoped<IRepository<EntityDomain>, EntityRepository>();
            services.AddScoped<IRepository<LanguageDomain>, LanguageRepository>();
            services.AddScoped<IRepository<AttributeDomain>, ContextRepository<AttributeDomain>>();
            services.AddScoped<IRepository<DataTypeDomain>, ContextRepository<DataTypeDomain>>();
            services.AddScoped<IRepository<LanguageDataTypeDomain>, ContextRepository<LanguageDataTypeDomain>>();

            //INFRA SERVICES    
            services.AddSingleton<IDynamicService,DynamicService>();
            services.AddSingleton<IDynamicRoutesService,DynamicRoutesService>();
            services.AddSingleton<ISwaggerRepository, SwaggerRepository>();

            //MESSAGING SERVICES
            services.AddScoped<IMsgManager, MsgManager>();

            //APP SERVICES
            services.AddScoped<EntityAppService>();
        }


    }
}
