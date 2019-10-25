using Domain.ValueObjects;

namespace InfrastructureTypes.Swagger
{
    public class MoneyDataType : DataTypeAbstract
    {
        public MoneyDataType() : base(EnumDataTypes.Money, "number", "float")
        {
        }   
    }
}