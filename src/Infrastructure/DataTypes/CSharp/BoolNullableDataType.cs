using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class BoolNullableDataType : DataTypeAbstract
    {
        public BoolNullableDataType() : base(EnumDataTypes.Bool, "bool?")
        {
        }
    }
}