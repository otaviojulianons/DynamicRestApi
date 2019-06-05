using Domain.ValueObjects;

namespace Infrastructure.DataTypes
{
    public interface IDataTypeFactory
    {
         IDataType Make(EnumDataTypes dataType, bool nullable);
    }
}