using Domain.Core.Implementation;
using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using Domain.Events;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : AggregateRoot, ISelfValidation<EntityDomain>
    {

        public EntityDomain(Name name)
        {
            Name = name;

            AddNotification(new AfterInsertEntityEvent(this));
            AddNotification(new AfterDeleteEntityEvent(this));
        }

        public IValidator<EntityDomain> Validator => new EntityValidator();

        public new Guid Id { get; private set; }

        public Name Name { get; private set; }

        private readonly List<AttributeDomain> _attributes = new List<AttributeDomain>();
        public IReadOnlyCollection<AttributeDomain> Attributes => _attributes;

        public void AddAttribute(Name name,bool allowNull, int? length,string genericType, DataTypeDomain dataType)
        {
            var attribute = new AttributeDomain(name,allowNull,length,genericType, dataType);
            _attributes.Add(attribute);
        }

    }
}
