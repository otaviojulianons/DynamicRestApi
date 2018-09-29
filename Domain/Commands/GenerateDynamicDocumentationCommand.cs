using Domain.Entities.EntityAggregate;
using MediatR;
using System.Collections.Generic;

namespace Domain.Commands
{
    public class GenerateDynamicDocumentationCommand : INotification
    {
        public GenerateDynamicDocumentationCommand(List<EntityDomain> entities)
        {
            Entities = entities;
        }

        public List<EntityDomain> Entities { get; private set; }
    }
}
