using Domain.Interfaces.Domain;

namespace Domain.ValueObjects
{
    public class DataTypeDomain : AggregateRoot
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool UseLength { get; set; }
    }
}
