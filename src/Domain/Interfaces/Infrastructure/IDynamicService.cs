using System;
using System.Collections.Generic;
using Domain.Entities.EntityAggregate;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicService
    {
        void GenerateControllerDynamic(IServiceProvider serviceProvider,IEnumerable<EntityDomain> entities);

        string GenerateSwaggerJsonFile(IEnumerable<EntityDomain> entities);

    }
}