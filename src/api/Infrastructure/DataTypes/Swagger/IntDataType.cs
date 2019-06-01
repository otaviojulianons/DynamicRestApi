using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class IntDataType : DataTypeAbstract
    {
        public IntDataType() : base(EnumDataTypes.Int, "integer", "int32")
        {
        }   
    }
}