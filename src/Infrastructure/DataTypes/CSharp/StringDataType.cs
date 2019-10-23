using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class StringDataType : DataTypeAbstract
    {
        public StringDataType() : base(EnumDataTypes.String, "string")
        {
        }   
    }
}