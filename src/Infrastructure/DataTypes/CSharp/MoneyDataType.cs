using Domain.ValueObjects;

namespace InfrastructureTypes.CSharp
{
    public class MoneyDataType : DataTypeAbstract
    {
        public MoneyDataType() : base(EnumDataTypes.Money, "decimal")
        {
        }   
    }
}