using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class StringDataType : DataTypeAbstract
    {
        public StringDataType() : base(EnumDataTypes.String, "string")
        {
        }   
    }
}