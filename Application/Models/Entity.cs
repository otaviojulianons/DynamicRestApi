using System;
using System.Collections.Generic;

namespace Application.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
