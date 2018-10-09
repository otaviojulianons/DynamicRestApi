using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Models;
using System;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicService
    {
        void GenerateControllerDynamic(IServiceProvider serviceProvider,params EntityTemplate[] entities);

        void GenerateSwaggerJsonFile(params EntityTemplate[] entities);

        object GetSwaggerJson();

    }
}