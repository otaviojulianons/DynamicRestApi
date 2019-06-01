using System.Data.Common;
using System;
using Domain.ValueObjects;
using Domain.Factories;
using Infrastructure.DataTypes.Swagger;

namespace Infrastructure.DataTypes.Factories
{
    public class SwaggerDataTypeFactory : IDataTypeFactory
    {
        public IDataType Make(EnumDataTypes dataType, bool nullable)      
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
    }
}