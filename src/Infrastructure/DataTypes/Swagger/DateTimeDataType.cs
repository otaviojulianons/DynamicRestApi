using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class DateTimeDataType : DataTypeAbstract
    {
        public DateTimeDataType() : base(EnumDataTypes.DateTime, "string", "date-time")
        {
        }        
    }
}