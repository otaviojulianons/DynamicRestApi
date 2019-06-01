using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class IdentifierDataType : DataTypeAbstract
    {
        public IdentifierDataType() : base(EnumDataTypes.DateTime, "string", "uuid")
        {
        }     
    }
}