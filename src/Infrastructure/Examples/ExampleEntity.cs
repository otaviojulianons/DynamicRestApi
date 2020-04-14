using Domain.Core.Interfaces.Structure;
using System;

namespace Infrastructure.Examples
{
    public class ExampleEntity : IGenericEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
