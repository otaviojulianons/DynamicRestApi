using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class IdentifierDataType : DataTypeAbstract
    {
        public IdentifierDataType() : base(EnumDataTypes.DateTime, "string", "uuid")
        {
        }     
    }
}