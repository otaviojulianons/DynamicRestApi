using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class MoneyDataType : DataTypeAbstract
    {
        public MoneyDataType() : base(EnumDataTypes.Money, "decimal")
        {
        }   
    }
}