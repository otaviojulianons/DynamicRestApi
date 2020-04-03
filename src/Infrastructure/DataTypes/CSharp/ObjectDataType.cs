using Domain.ValueObjects;
using InfrastructureTypes;

namespace InfrastructureTypes.CSharp
{
    public class ObjectDataType : DataTypeAbstract
    {
        public ObjectDataType(string parameter) : base(EnumDataTypes.Object, parameter, parameter)
        {
        }
    }
}
