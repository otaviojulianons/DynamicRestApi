using Domain.Core.Extensions;
using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using Domain.ValueObjects;
using FluentValidation;
using Infrastructure.CrossCutting.Notifications;
using System;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeDomain : IEntity , ISelfValidation
    {

        public AttributeDomain(Name name, EnumDataTypes dataType, bool allowNull = false, int? length = null)
        {
            Name = name;
            AllowNull = allowNull;
            Length = length;
            DataType = dataType;
        }

        protected AttributeDomain(Guid id, Name name, EnumDataTypes dataType, bool allowNull, int? length, EntityDomain entity)
            : this(name,dataType, allowNull, length) 
        {
            this.Id = id;
        }
        
        public Guid Id { get; private set; }

        public Name Name { get; private set; }

        public int? Length { get; private set; }

        public bool AllowNull { get; private set; }

        public EnumDataTypes DataType { get; private set; }

        public bool IsIdentifier => DataType == EnumDataTypes.Identifier;

        public bool IsValid(INotificationManager notifications) =>
            new AttributeValidator().IsValid(this, notifications);
    }
}
