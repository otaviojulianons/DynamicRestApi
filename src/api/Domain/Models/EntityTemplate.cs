using Domain.Entities.EntityAggregate;
using Domain.Factories;
using Infrastructure.CrossCutting.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    public class EntityTemplate
    {
        public EntityTemplate(
            EntityDomain entity,
            IDataTypeFactory dataTypeFactory
            )
        {
            Name = entity.Name;
            var attributesTemplate = new List<AttributeTemplate>();
            foreach (var attribute in entity.Attributes.Where(item => !item.IsIdentifier))
            {
                var dataType = dataTypeFactory.Make(attribute.DataType, attribute.AllowNull);
                attributesTemplate.Add(new AttributeTemplate(attribute, dataType));
            }
            Attributes = new TemplateCollection<AttributeTemplate>(attributesTemplate);
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<TemplateCollectionItem<AttributeTemplate>> Attributes { get; private set; }

    }
}
