using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class BoolDataType : DataTypeAbstract
    {
        public BoolDataType() : base(EnumDataTypes.Bool, "boolean", "boolean")
        {
        }
    }
}