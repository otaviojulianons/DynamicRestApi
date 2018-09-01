using SharedKernel.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class DataTypeDomain : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool UseLength { get; set; }
    }
}
