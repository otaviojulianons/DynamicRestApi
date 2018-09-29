using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.EntityAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicService
    {
        void GenerateControllerDynamic(IServiceProvider serviceProvider, params EntityDomain[] entities);

        void GenerateSwaggerJsonFile(params EntityDomain[] entities);

        string GenerateSwaggerJson(params EntityDomain[] entities);

        object GetSwaggerJson();

        Task Init(IServiceScopeFactory serviceScopeFactory);
    }
}