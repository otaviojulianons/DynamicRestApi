using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.CrossCutting.Notifications;
using Infrastructure.Data.GraphQL;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories;
using Infrastructure.Data.Repository.Repositoritemplate;
using Infrastructure.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.IoC
{
    public class IoCService
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
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
