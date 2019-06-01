using System.Data.Common;
using System;
using Domain.ValueObjects;
using Domain.Factories;
using Infrastructure.DataTypes.CSharp;

namespace Infrastructure.DataTypes.Factories
{
    public class CSharpDataTypeFactory : IDataTypeFactory
    {
        public IDataType Make(EnumDataTypes dataType, bool nullable) =>
            nullable ? MakeNullable(dataType) : Make(dataType);  

        public IDataType Make(EnumDataTypes dataType)        
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

        public IDataType MakeNullable(EnumDataTypes dataType)        
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
    }
}