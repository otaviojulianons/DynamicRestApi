using Domain.Entities.EntityAggregate;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IDocumentationDomainService
    {
        void GenerateDocumentation(IEnumerable<EntityDomain> entities);
    }
}
