using SharedKernel.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Domain
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

        public List<LanguageDataTypeDomain> DataTypes { get; set; }
    }
}
