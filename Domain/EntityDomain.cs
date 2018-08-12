using SharedKernel.Repository;
using System.Collections.Generic;

namespace Domain
{
    public class EntityDomain : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public List<AttributeDomain> Attributes { get; set; }

    }
}
