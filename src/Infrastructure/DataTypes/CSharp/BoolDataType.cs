using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class BoolDataType : DataTypeAbstract
    {
        public BoolDataType() : base(EnumDataTypes.Bool, "bool")
        {
        }
    }
}