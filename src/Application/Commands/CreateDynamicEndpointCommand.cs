using Domain.Entities.EntityAggregate;
using MediatR;
using System.Collections.Generic;

namespace Application.Commands
{
    public class CreateDynamicEndpointCommand : IRequest<bool>
    {
        public IEnumerable<EntityDomain> Entities { get; private set; }

        public CreateDynamicEndpointCommand(EntityDomain entity)
        {
            Entities = new[] { entity };
        }

        public CreateDynamicEndpointCommand(IEnumerable<EntityDomain> entities)
        {
            Entities = entities;
        }
    }
}
