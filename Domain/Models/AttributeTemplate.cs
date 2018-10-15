using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class AttributeTemplate
    {
        public AttributeTemplate(
            AttributeDomain attribute,
            LanguageDomain language
            )
        {
            Name = attribute.Name;
            Length = attribute.Length;
            AllowNull = attribute.AllowNull;
            TypeLanguage = language.GetTypeLanguage(attribute.DataType.Name, attribute.AllowNull);
        }

        public string Name { get; private set; }

        public int? Length { get; private set; }

        public bool AllowNull { get; private set; }

        public TypeLanguage TypeLanguage { get; private set; }
    }
}
