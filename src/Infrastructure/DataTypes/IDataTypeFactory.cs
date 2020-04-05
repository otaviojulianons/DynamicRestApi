using Domain.ValueObjects;

namespace InfrastructureTypes
{
    public interface IDataTypeFactory
    {
        IDataType MakeValueDataType(EnumDataTypes dataType, bool nullable = false);
        IDataType MakeGenericDataType(EnumDataTypes dataType, string parameter);
    }
}