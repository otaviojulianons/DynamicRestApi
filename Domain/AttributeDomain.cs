using SharedKernel.Repository;

namespace Domain
{
    public class AttributeDomain : IEntity
    {

        public long Id { get; set; }
        public long EntityId { get; set; }
        public long DataTypeId { get; set; }
        public string Name { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }
        public bool PrimaryKey { get; set; }
        public bool ForeignKey { get; set; }
        public string ForeignEntity { get; set; }
        public string ForeignAttribute { get; set; }
        public DataTypeDomain DataType { get; set; }
        public EntityDomain Entity { get; set; }

    }
}
