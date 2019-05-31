using Domain.Core.Interfaces.Structure;

namespace Domain.ValueObjects
{
    public interface IDataType
    {        
        EnumDataTypes DataType { get; } 
        string Name { get; }
        string Format { get; }  
    }
}