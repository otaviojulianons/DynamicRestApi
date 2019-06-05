using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class DateTimeDataType : DataTypeAbstract
    {
        public DateTimeDataType() : base(EnumDataTypes.DateTime, "DateTime")
        {
        }        
    }
}