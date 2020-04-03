using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class LongNullableDataType : DataTypeAbstract
    {
        public LongNullableDataType() : base(EnumDataTypes.Long, "long?")
        {
        }
    }
}