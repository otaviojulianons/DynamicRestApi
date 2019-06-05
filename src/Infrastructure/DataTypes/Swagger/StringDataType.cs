using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class StringDataType : DataTypeAbstract
    {
        public StringDataType() : base(EnumDataTypes.String, "string","string")
        {
        }   
    }
}