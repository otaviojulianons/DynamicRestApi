using System;
using System.Collections.Generic;
using Domain.Entities.EntityAggregate;

namespace Domain.Services
{
    public interface IDynamicDomainService
    {

        void GenerateTypes(params EntityDomain[] entities);
    }
}