using SharedKernel.Repository;

namespace Domain
{
    public class AttributeDomain : IEntity
    {

        public long Id { get; set; }
        public long EntityId { get; set; }
        public long DataTypeId { get; set; }
        public int Length { get; set; }
        public bool AllowNull { get; set; }
        public bool PrimaryKey { get; set; }
        public string ForeignKey { get; set; }
        public string ForeignEntity { get; set; }
        public string ForeignAttribute { get; set; }  

    }
}
