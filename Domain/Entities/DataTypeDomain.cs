using Domain.Core.Interfaces.Structure;

namespace Domain.Entities
{
    public class DataTypeDomain : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool UseLength { get; set; }

    }
}
