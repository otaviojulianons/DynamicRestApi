using Domain.Interfaces;

namespace Domain.Entities.LanguageAggregate
{
    public class LanguageDataTypeDomain : IEntity
    {
        public long Id { get; set; }

        public long LanguageId { get; set; }

        public long DataTypeId { get; set; }

        public string Name { get; set; }

        public string NameNullable { get; set; }

        public string Format { get; set; }

        public LanguageDomain Language { get; set; }
    }
}
