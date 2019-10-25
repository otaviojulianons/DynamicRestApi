using Domain.ValueObjects;

namespace InfrastructureTypes
{
    public interface IDataTypeFactory
    {
         IDataType Make(EnumDataTypes dataType, bool nullable);
    }
}