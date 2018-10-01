using System.Collections.Generic;

namespace Application.Models
{
    public class Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
