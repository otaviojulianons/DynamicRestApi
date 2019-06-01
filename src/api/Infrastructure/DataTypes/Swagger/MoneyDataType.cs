using Domain.ValueObjects;

namespace Infrastructure.DataTypes.Swagger
{
    public class MoneyDataType : DataTypeAbstract
    {
        public MoneyDataType() : base(EnumDataTypes.Money, "number", "float")
        {
        }   
    }
}