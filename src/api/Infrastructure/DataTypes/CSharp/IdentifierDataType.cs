using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class IdentifierDataType : DataTypeAbstract
    {
        public IdentifierDataType() : base(EnumDataTypes.DateTime, "Guid")
        {
        }     
    }
}