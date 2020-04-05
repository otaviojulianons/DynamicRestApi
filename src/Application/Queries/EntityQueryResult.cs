using System;
using System.Collections.Generic;

namespace Application.Queries
{
    public class EntityQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AttributeQueryResult> Attributes { get; set; }
        public List<ElementQueryResult> Elements { get; set; }
    }
}
