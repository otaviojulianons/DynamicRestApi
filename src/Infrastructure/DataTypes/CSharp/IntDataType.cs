using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class IntDataType : DataTypeAbstract
    {
        public IntDataType() : base(EnumDataTypes.Int, "int")
        {
        }   
    }
}