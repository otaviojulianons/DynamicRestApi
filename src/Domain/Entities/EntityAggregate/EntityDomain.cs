using Domain.Core.Extensions;
using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using FluentValidation;
using Common.Notifications;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : ISelfValidation
    {
        public EntityDomain(){}
        public EntityDomain(Name name)
        {
            Name = name;
        }

        public Guid Id { get; set; }
        public Name Name { get; set; }   

        public string NameString => Name.ToString();
        private List<AttributeDomain> _attributes = new List<AttributeDomain>();
        public IReadOnlyCollection<AttributeDomain> Attributes => _attributes;

        public void AddAttribute(AttributeDomain attribute) => _attributes.Add(attribute);

        public bool IsValid(INotificationManager notifications) =>
            new EntityValidator().IsValid(this, notifications);

    }
}
