using Domain.Entities.EntityAggregate;
using MediatR;

namespace Domain.Events
{
    public class CreateDynamicControllerEvent : INotification
    {
        public CreateDynamicControllerEvent(EntityDomain entity)
        {
            Entity = entity;
        }

        public EntityDomain Entity { get; }
    }
}
