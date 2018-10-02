using Domain.Entities.LanguageAggregate;
using Domain.Events;
using Domain.Helpers.Collections;
using Domain.Interfaces.Domain;
using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.EntityAggregate
{
    public class EntityDomain : AggregateRoot, ISelfValidation<EntityDomain>
    {
        public EntityDomain(string name)
        {
            Name = name;

            AddNotification(new AfterInsertEntityEvent(this));
        }

        public IValidator<EntityDomain> Validator => new EntityValidator();

        public new long Id { get; private set; }

        public string Name { get; private set; }

        public List<AttributeDomain> Attributes { get; private set; }

        public NavigableList<AttributeDomain> AttributesNavigable => Attributes.ToNavigableList();

        public void DefineDataTypes(IEnumerable<DataTypeDomain> dataTypes)
        {
            foreach (var attribute in Attributes)
                attribute.DataTypeId = dataTypes.FirstOrDefault(type => type.Name == attribute.DataTypeName)?.Id ?? 0;
        }

        public void DefineLanguage(LanguageDomain language)
        {
            foreach (var attribute in Attributes)
                attribute.TypeLanguage = language.GetTypeLanguage(attribute.DataTypeId, attribute.AllowNull);
        }

    }
}
