using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class LongNullableDataType : DataTypeAbstract
    {
        public LongNullableDataType() : base(EnumDataTypes.Bool, "long?")
        {
        }
    }
}