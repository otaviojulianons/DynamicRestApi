using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class MoneyNullableDataType : DataTypeAbstract
    {
        public MoneyNullableDataType() : base(EnumDataTypes.Money, "decimal?")
        {
        }   
    }
}