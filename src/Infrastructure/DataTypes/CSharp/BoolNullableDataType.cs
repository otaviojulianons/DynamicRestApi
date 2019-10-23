using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class BoolNullableDataType : DataTypeAbstract
    {
        public BoolNullableDataType() : base(EnumDataTypes.Bool, "bool?")
        {
        }
    }
}