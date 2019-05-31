using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class MoneyNullableDataType : DataTypeAbstract
    {
        public MoneyNullableDataType() : base(EnumDataTypes.Money, "decimal?")
        {
        }   
    }
}