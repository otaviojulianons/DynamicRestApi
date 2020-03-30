using Domain.ValueObjects;

namespace Application.Queries
{
    public class AttributeQueryResult
    {
        public string Name { get; set; }
        public EnumDataTypes DataType { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }
    }
}
