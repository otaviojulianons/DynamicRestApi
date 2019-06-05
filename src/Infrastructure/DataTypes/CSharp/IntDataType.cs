using Domain.ValueObjects;

namespace Infrastructure.DataTypes.CSharp
{
    public class IntDataType : DataTypeAbstract
    {
        public IntDataType() : base(EnumDataTypes.Int, "int")
        {
        }   
    }
}