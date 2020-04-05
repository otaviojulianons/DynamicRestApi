using Domain.ValueObjects;
using InfrastructureTypes;

namespace InfrastructureTypes.CSharp
{
    public class ArrayDataType : DataTypeAbstract
    {
        public ArrayDataType(string parameter) : base(EnumDataTypes.Object, $"IEnumerable<{parameter}>", parameter)
        {
        }
    }
}
