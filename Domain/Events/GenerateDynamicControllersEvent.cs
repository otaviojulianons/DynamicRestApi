using Domain.Entities.EntityAggregate;
using MediatR;

namespace Domain.Events
{
    public class GenerateDynamicControllersEvent : INotification
    {
        public EntityDomain Entity { get; private set; }

        public GenerateDynamicControllersEvent(EntityDomain entity = null)
        {
            Entity = entity;
        }
    }
}
