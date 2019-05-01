using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using System;

namespace Domain.Entities
{
    public class DataTypeDomain : IEntity
    {
        public DataTypeDomain(Name name, bool useLength)
        {
            Name = name;
            UseLength = useLength;
        }

        public Guid Id { get; private set; }

        public Name Name { get; private set; }

        public bool UseLength { get; private set; }

    }
}
