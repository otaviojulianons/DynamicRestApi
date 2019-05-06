using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Infrastructure.CrossCutting.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    public class EntityTemplate
    {
        public EntityTemplate(
            EntityDomain entity,
            LanguageDomain language
            )
        {
            Name = entity.Name;

            var attributesTemplate = entity.Attributes
                .Where(item => !item.IsIdentifier)
                .Select(item => new AttributeTemplate(item, language));

            Attributes = new NavigableList<AttributeTemplate>(attributesTemplate);
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<NavigableItem<AttributeTemplate>> Attributes { get; private set; }

    }
}
