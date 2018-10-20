using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;

namespace Domain.Entities
{
    public class DataTypeDomain : IEntity
    {
        public DataTypeDomain(Name name, bool useLength)
        {
            Name = name;
            UseLength = useLength;
        }

        public long Id { get; private set; }

        public Name Name { get; private set; }

        public bool UseLength { get; private set; }

    }
}
