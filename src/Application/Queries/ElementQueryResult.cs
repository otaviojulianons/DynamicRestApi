using Domain.ValueObjects;

namespace Application.Queries
{
    public class ElementQueryResult
    {
        public EnumDataTypes DataType { get; set; }
        public EntityQueryResult Entity { get; set; }
    }
}
