using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using Infrastructure.Dynamic;
using Infrastructure.GraphQL;
using Infrastructure.Repository.Contexts;
using Infrastructure.Repository.Repositories;
using Infrastructure.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public class Bootstrap
    {
        public static void Run(IServiceCollection services)
        {
            //INFRA REPOSITORY
            services.AddSingleton<ContextMongodb>();
            services.AddSingleton<DatabaseRepository>();
            services.AddSingleton<SwaggerRepository>();
            services.AddScoped<GraphQLRepository>();
            services.AddScoped<IRepository<EntityDomain>, EntityRepository>();           
            services.AddScoped(typeof(IRepository<>), typeof(MongodbRepository<>));

            //INFRA SERVICES    
            services.AddSingleton<IDynamicDomainService,DynamicService>();
            services.AddSingleton<IDocumentationDomainService, SwaggerService>();
            services.AddSingleton<DynamicRoutesService>();
            
            //MESSAGING SERVICES
            services.AddScoped<INotificationManager, NotificationManager>();

        }


    }
}
