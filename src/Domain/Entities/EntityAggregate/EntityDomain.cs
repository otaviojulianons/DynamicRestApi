using Common.Notifications;
using Domain.Core.Extensions;
using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : ISelfValidation
    {
        private List<AttributeDomain> _attributes = new List<AttributeDomain>();
        private List<ElementDomain> _elements = new List<ElementDomain>();

        public EntityDomain(){}
        public EntityDomain(Name name)
        {
            Name = name;
        }

        public Guid Id { get; set; }

        public Name Name { get; set; }   

        public IReadOnlyCollection<AttributeDomain> Attributes => _attributes;

        public IReadOnlyCollection<ElementDomain> Elements => _elements;

        public void AddAttribute(AttributeDomain attribute) => _attributes.Add(attribute);

        public void AddElement(ElementDomain element) => _elements.Add(element);

        public bool IsValid(INotificationManager notifications) =>
            new EntityValidator().IsValid(this, notifications);

    }
}
