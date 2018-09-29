using Domain.Commands;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Helpers.Collections;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Structure;
using Domain.Services;
using Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Contexts;
using Repository.Repositories;
using Services;
using SharedKernel.Notifications;

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
            services.AddSingleton<INotificationHandler<GenerateDynamicDocumentationCommand>, DynamicService>();
            services.AddSingleton<IDynamicRoutesService,DynamicRoutesService>();

            services.AddScoped<IMsgManager, MsgManager>();
            services.AddScoped<DataTypeService>();
            services.AddScoped<LanguageService>();
            services.AddScoped<EntityService>();
        }


    }
}
