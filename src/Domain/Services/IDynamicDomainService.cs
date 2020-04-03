using System;
using System.Collections.Generic;
using Domain.Entities.EntityAggregate;

namespace Domain.Services
{
    public interface IDynamicDomainService
    {
        IEnumerable<Type> Controllers { get; }
        IEnumerable<Type> Entities { get; }

        void GenerateTypes(params EntityDomain[] entities);
    }
}