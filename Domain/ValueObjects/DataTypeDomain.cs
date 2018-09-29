using Domain.Interfaces.Structure;

namespace Domain.ValueObjects
{
    public class DataTypeDomain : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool UseLength { get; set; }
    }
}
