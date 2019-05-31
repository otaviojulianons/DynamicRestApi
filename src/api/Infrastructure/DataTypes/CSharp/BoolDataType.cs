using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class BoolDataType : DataTypeAbstract
    {
        public BoolDataType() : base(EnumDataTypes.Bool, "bool")
        {
        }
    }
}