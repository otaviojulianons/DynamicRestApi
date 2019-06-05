using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class LongNullableDataType : DataTypeAbstract
    {
        public LongNullableDataType() : base(EnumDataTypes.Bool, "long?")
        {
        }
    }
}