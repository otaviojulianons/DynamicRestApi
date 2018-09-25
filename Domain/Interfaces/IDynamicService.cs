using System;
using System.Collections.Generic;
using Domain.Entities.EntityAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Interfaces
{
    public interface IDynamicService
    {
        string GenerateClassDomainFromEntity(EntityDomain entity);
        void GenerateControllerDynamic(IServiceProvider serviceProvider, EntityDomain entity);
        void GenerateControllerDynamic(IServiceProvider serviceProvider, List<EntityDomain> entities);
        void GenerateSwaggerFile(List<EntityDomain> entities);
        string GenerateSwaggerFileFromEntity(List<EntityDomain> entities);
        dynamic GenerateTypeFromCode(string classCode);
        object GetJsonSwagger();
        void Init(IServiceScopeFactory serviceScopeFactory);
    }
}