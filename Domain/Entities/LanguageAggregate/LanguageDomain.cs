using Domain.Interfaces.Structure;
using Domain.ValueObjects;
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

        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<LanguageDataTypeDomain> DataTypes { get; set; }

        public AttributeTypeLanguage GetTypeLanguage(long idDataType, bool nullable)
        {
            var dataType = DataTypes.FirstOrDefault(x => x.DataTypeId == idDataType);
            if (dataType == null)
                return null;

            return new AttributeTypeLanguage()
            {
                Format = dataType.Format,
                Type = nullable ? dataType.NameNullable ?? dataType.Name : dataType.Name
            };    
        }
    }
}
