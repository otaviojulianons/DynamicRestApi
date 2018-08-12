using SharedKernel.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("DataTypes")]
    public class DataTypeDomain : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool UseLength { get; set; }

    }
}
