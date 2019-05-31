using Domain.Core.Extensions;
using Domain.Core.Implementation;
using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using Domain.Events;
using FluentValidation;
using Infrastructure.CrossCutting.Notifications;
using System;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : AggregateRoot, ISelfValidation
    {

        public EntityDomain(Name name)
        {
            Name = name;

            AddNotification(new AfterInsertEntityEvent(this));
            AddNotification(new AfterDeleteEntityEvent(this));
        }

        public new Guid Id { get; private set; }

        public Name Name { get; private set; }

        private readonly List<AttributeDomain> _attributes = new List<AttributeDomain>();
        
        public IReadOnlyCollection<AttributeDomain> Attributes => _attributes;

        public void AddAttribute(AttributeDomain attribute) => _attributes.Add(attribute);

        public bool IsValid(INotificationManager notifications) =>
            new EntityValidator().IsValid(this, notifications);        

    }
}
