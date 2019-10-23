using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class IdentifierDataType : DataTypeAbstract
    {
        public IdentifierDataType() : base(EnumDataTypes.DateTime, "Guid")
        {
        }     
    }
}