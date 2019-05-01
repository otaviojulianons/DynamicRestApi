using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using System;

namespace Domain.Entities.LanguageAggregate
{
    public class LanguageDataTypeDomain : IEntity
    {
        public Guid Id { get; private set; }

        public Name Name { get; private set; }

        public Name NameNullable { get; private set; }

        public string Format { get; private set; }

        public LanguageDomain Language { get; private set; }

        public DataTypeDomain DataType { get; private set; }
    }
}
