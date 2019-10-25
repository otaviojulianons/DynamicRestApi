using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class DateTimeDataType : DataTypeAbstract
    {
        public DateTimeDataType() : base(EnumDataTypes.DateTime, "DateTime")
        {
        }        
    }
}