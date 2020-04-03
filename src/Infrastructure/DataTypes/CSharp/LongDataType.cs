using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class LongDataType : DataTypeAbstract
    {
        public LongDataType() : base(EnumDataTypes.Long, "long")
        {
        }
    }
}