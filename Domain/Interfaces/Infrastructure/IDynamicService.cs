using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using System;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicService
    {
        void GenerateControllerDynamic(IServiceProvider serviceProvider,LanguageDomain language,params EntityDomain[] entities);

        void GenerateSwaggerJsonFile(LanguageDomain language,params EntityDomain[] entities);

        object GetSwaggerJson();

    }
}