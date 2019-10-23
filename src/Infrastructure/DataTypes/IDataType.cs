using Domain.Core.Interfaces.Structure;
using Domain.ValueObjects;

namespace InfrastructureTypes
{
    public interface IDataType
    {        
        EnumDataTypes DataType { get; } 
        string Name { get; }
        string Format { get; }  
    }
}