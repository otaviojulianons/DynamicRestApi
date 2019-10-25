using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class IntDataType : DataTypeAbstract
    {
        public IntDataType() : base(EnumDataTypes.Int, "integer", "int32")
        {
        }   
    }
}