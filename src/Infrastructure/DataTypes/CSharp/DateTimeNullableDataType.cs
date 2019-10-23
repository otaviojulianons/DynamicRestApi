using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class DateTimeNullableDataType : DataTypeAbstract
    {
        public DateTimeNullableDataType() : base(EnumDataTypes.DateTime, "DateTime?")
        {
        }        
    }
}