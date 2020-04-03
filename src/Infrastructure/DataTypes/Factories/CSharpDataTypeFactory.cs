using Domain.ValueObjects;
using InfrastructureTypes.CSharp;

namespace InfrastructureTypes.Factories
{
    public class CSharpDataTypeFactory : IDataTypeFactory
    {
        public IDataType MakeValueDataType(EnumDataTypes dataType, bool nullable) =>
            nullable ? MakeNullableDataType(dataType) : MakeValueDataType(dataType);  

        private IDataType MakeValueDataType(EnumDataTypes dataType)        
        {           
            switch (dataType)
            {
                case EnumDataTypes.Bool:
                    return new BoolDataType();
                case EnumDataTypes.DateTime:
                    return new DateTimeDataType();
                case EnumDataTypes.Identifier:
                    return new IdentifierDataType();
                case EnumDataTypes.Int:
                    return new IntDataType();
                case EnumDataTypes.Long:
                    return new LongDataType();                      
                case EnumDataTypes.Money:
                    return new MoneyDataType();
                case EnumDataTypes.String:
                    return new StringDataType();                                                                                                    
                default:
                    return null;
            }
        }

        private IDataType MakeNullableDataType(EnumDataTypes dataType)        
        {
            switch (dataType)
            {
                case EnumDataTypes.Bool:
                    return new BoolNullableDataType();
                case EnumDataTypes.DateTime:
                    return new DateTimeDataType();
                case EnumDataTypes.Identifier:
                    return new IdentifierDataType();
                case EnumDataTypes.Int:
                    return new IntNullableDataType();
                case EnumDataTypes.Long:
                    return new LongNullableDataType();                    
                case EnumDataTypes.Money:
                    return new MoneyNullableDataType();
                case EnumDataTypes.String:
                    return new StringDataType();                                                                                                    
                default:
                    return null;
            }
        }

        public IDataType MakeGenericDataType(EnumDataTypes dataType, string parameter)
        {
            switch (dataType)
            {
                case EnumDataTypes.Object:
                    return new ObjectDataType(parameter);
                case EnumDataTypes.Array:
                    return new ArrayDataType(parameter);
                default:
                    return null;
            }
        }
    }
}