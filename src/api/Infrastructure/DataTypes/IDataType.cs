using Domain.Core.Interfaces.Structure;
using Domain.ValueObjects;

namespace Infrastructure.DataTypes
{
    public interface IDataType
    {        
        EnumDataTypes DataType { get; } 
        string Name { get; }
        string Format { get; }  
    }
}