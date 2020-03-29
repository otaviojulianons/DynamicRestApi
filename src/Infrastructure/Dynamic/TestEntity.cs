using Domain.Core.Interfaces.Structure;
using System;

namespace Infrastructure.Dynamic
{
    public class TestEntity : IGenericEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
