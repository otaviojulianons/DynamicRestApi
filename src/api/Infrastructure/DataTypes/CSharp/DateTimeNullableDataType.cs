using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class DateTimeNullableDataType : DataTypeAbstract
    {
        public DateTimeNullableDataType() : base(EnumDataTypes.DateTime, "DateTime?")
        {
        }        
    }
}