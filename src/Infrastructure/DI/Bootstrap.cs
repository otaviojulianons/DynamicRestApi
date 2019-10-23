using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Common.Notifications;
using Infrastructure.GraphQL;
using Infrastructure.Repository.Contexts;
using Infrastructure.Repository.Repositories;
using Infrastructure.Repository.Repositoritemplate;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;

namespace Infrastructure.DI
{
    public class Bootstrap
    {
        public static void Run(IServiceCollection services)
        {
            //INFRA REPOSITORY
            services.AddSingleton<ContextMongodb>();
            services.AddScoped<GraphQLRepository>();
            services.AddScoped<IRepository<EntityDomain>, EntityRepository>();           
            services.AddScoped(typeof(IRepository<>), typeof(MongodbRepository<>));

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
