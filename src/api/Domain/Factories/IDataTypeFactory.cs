using Domain.ValueObjects;

namespace Domain.Factories
{
    public interface IDataTypeFactory
    {
         IDataType Make(EnumDataTypes dataType, bool nullable);
    }
}