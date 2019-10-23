using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class IntNullableDataType : DataTypeAbstract
    {
        public IntNullableDataType() : base(EnumDataTypes.Int, "int?")
        {
        }   
    }
}