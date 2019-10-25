using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class LongDataType : DataTypeAbstract
    {
        public LongDataType() : base(EnumDataTypes.Bool, "integer", "int64")
        {
        }
    }
}