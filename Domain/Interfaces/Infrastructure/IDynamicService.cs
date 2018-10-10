using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Models;
using System;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicService
    {
        void GenerateControllerDynamic(IServiceProvider serviceProvider,params EntityTemplate[] entities);

        string GenerateSwaggerJsonFile(params EntityTemplate[] entities);

    }
}