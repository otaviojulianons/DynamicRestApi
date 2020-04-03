using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using Infrastructure.Dynamic;
using Infrastructure.GraphQL;
using Infrastructure.Repository.Contexts;
using Infrastructure.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public class Bootstrap
    {
        public static void Run(IServiceCollection services)
        {
            //INFRA REPOSITORY
            services.AddSingleton<MongodbContext>();
            services.AddSingleton<DatabaseRepository>();
            services.AddScoped<GraphQLRepository>();
            services.AddScoped<IRepository<EntityDomain>, EntityRepository>();           
            services.AddScoped(typeof(IRepository<>), typeof(MongodbRepository<>));
            services.AddScoped(typeof(IGenericRepository<,>), typeof(MongodbGenericRepository<,>));

            //INFRA SERVICES    
            services.AddSingleton<IDynamicDomainService,DynamicService>();
            
            //MESSAGING SERVICES
            services.AddScoped<INotificationManager, NotificationManager>();

        }


    }
}
