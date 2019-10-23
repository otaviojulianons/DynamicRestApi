using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class IntNullableDataType : DataTypeAbstract
    {
        public IntNullableDataType() : base(EnumDataTypes.Int, "int?")
        {
        }   
    }
}