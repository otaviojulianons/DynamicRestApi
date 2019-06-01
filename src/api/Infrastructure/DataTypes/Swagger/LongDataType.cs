using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class LongDataType : DataTypeAbstract
    {
        public LongDataType() : base(EnumDataTypes.Bool, "integer", "int64")
        {
        }
    }
}