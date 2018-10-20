using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.LanguageAggregate
{
    public class LanguageDomain : IEntity
    {
        public enum EnumLanguages
        {
            Csharp = 1,
            SwaggerDoc = 2
        }

        public LanguageDomain(Name name)
        {
            Name = name;
        }

        public long Id { get; private set; }

        public Name Name { get; private set; }

        public IReadOnlyCollection<LanguageDataTypeDomain> DataTypes { get; set; }

        public TypeLanguage GetTypeLanguage(string dataTypeName, bool nullable)
        {
            var languageDataType = DataTypes.FirstOrDefault(x => x.DataType.Name == dataTypeName) 
                ?? throw new Exception($"Invalid data type  {dataTypeName}.");

            return new TypeLanguage(languageDataType,nullable);    
        }
    }
}
