﻿using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Events;

namespace Domain.Events
{
    public class AfterInsertEntityEvent : IAfterInsertDomainEvent<EntityDomain>
    {
        public AfterInsertEntityEvent(EntityDomain entity)
        {
            Entity = entity;
        }

        public EntityDomain Entity { get; private set; }
    }
}
