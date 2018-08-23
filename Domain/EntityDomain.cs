using SharedKernel.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class EntityDomain : Navigable, INavigable, IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<AttributeDomain> Attributes { get; set; }

    }
}
