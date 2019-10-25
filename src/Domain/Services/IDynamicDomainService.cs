using System;
using System.Collections.Generic;
using Domain.Entities.EntityAggregate;

namespace Domain.Services
{
    public interface IDynamicDomainService
    {
        IEnumerable<Type> DynamicTypes { get; }

        void GenerateType(params EntityDomain[] entities);

        void RemoveType(params EntityDomain[] entities);
    }
}