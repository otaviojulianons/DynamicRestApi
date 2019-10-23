using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class StringDataType : DataTypeAbstract
    {
        public StringDataType() : base(EnumDataTypes.String, "string","string")
        {
        }   
    }
}