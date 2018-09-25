using Domain.Helpers.Collections;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<AttributeDomain> Attributes { get; set; }

        public NavigableList<AttributeDomain> AttributesNavigable => Attributes.ToNavigableList();
    }
}
