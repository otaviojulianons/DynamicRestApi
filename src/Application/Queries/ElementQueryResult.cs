using Domain.ValueObjects;

namespace Application.Queries
{
    public class ElementQueryResult
    {
        public EnumElementType DataType { get; set; }
        public EntityQueryResult Entity { get; set; }
    }
}
