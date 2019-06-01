using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.CrossCutting.Notifications;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories.Application;
using Infrastructure.Data.Repository.Repositories.Memory;
using Infrastructure.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.IoC
{
    public class IoCService
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            //REPOSITORY
            services.AddDbContext<DbContextSqlServer>();
            services.AddScoped<DbContextMongo>();
            services.AddScoped(typeof(IDynamicRepository<>), typeof(RepositoryMongodb<>));

            services.AddScoped<IRepository<EntityDomain>, EntityRepository>();
            services.AddScoped<IRepository<AttributeDomain>, RepositorySqlServer<AttributeDomain>>();

            //INFRA SERVICES    
            services.AddSingleton<IDynamicService,DynamicService>();
            services.AddSingleton<IDynamicRoutesService,DynamicRoutesService>();
            services.AddSingleton<IDocumentationRepository, DocumentationRepository>();
            services.AddSingleton<IDatabaseService, DatabaseService>();

            //MESSAGING SERVICES
            services.AddScoped<INotificationManager, NotificationManager>();

        }


    }
}
