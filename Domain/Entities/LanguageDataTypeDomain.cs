using SharedKernel.Repository;

namespace Domain
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
