using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class BoolDataType : DataTypeAbstract
    {
        public BoolDataType() : base(EnumDataTypes.Bool, "boolean", "boolean")
        {
        }
    }
}