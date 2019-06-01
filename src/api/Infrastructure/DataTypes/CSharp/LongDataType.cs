using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class LongDataType : DataTypeAbstract
    {
        public LongDataType() : base(EnumDataTypes.Bool, "long")
        {
        }
    }
}