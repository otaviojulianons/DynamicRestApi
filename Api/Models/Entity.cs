using System.Collections.Generic;

namespace Api.Models
{
    public class Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
