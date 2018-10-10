using Domain.Base;
using Domain.Events;
using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : AggregateRoot, ISelfValidation<EntityDomain>
    {
        public EntityDomain(string name)
        {
            Name = name;

            AddNotification(new AfterInsertEntityEvent(this));
            AddNotification(new AfterDeleteEntityEvent(this));
        }

        public IValidator<EntityDomain> Validator => new EntityValidator();

        public new long Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<AttributeDomain> Attributes { get; private set; }

        public void DefineDataTypes(IEnumerable<DataTypeDomain> dataTypes)
        {
            foreach (var attribute in Attributes)
                attribute.DataTypeId = dataTypes.FirstOrDefault(type => type.Name == attribute.DataTypeName)?.Id ?? 0;
        }



    }
}
