using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class DateTimeDataType : DataTypeAbstract
    {
        public DateTimeDataType() : base(EnumDataTypes.DateTime, "string", "date-time")
        {
        }        
    }
}