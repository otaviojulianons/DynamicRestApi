using Domain.Helpers.Collections;
using Domain.Interfaces.Structure;
using FluentValidation;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : ISelfValidation<EntityDomain>
    {
        public IValidator<EntityDomain> Validator => new EntityValidator();

        public long Id { get; private set; }

        public string Name { get; private set; }

        public List<AttributeDomain> Attributes { get; private set; }

        public NavigableList<AttributeDomain> AttributesNavigable => Attributes.ToNavigableList();
  
    }
}
