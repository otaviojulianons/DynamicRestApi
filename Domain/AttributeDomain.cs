using SharedKernel.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AttributeDomain : Navigable, IEntity
    {

        public long Id { get; set; }
        public long EntityId { get; set; }
        public long DataTypeId { get; set; }
        public string Name { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }

        public DataTypeDomain DataType { get; set; }
        public EntityDomain Entity { get; set; }




    }
}
