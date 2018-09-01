using SharedKernel.Repository;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class EntityDomain : Navigable, IEntity, IDomainValidation
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<AttributeDomain> Attributes { get; set; }

        public void Validate()
        {
            if(string.IsNullOrEmpty(Name))
                throw new Exception("Entity name is invalid!");
            if (Attributes?.Count == 0)
                throw new Exception("Invalid attributes!");
            if (Attributes.Find( item => item.Name == "Id" && item.DataTypeName == "long") == null)
                throw new Exception("Attribute Id not found!");
        }

    }
}
