using Domain.Entities.EntityAggregate;
using Domain.Factories;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Templates
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
            Attributes = new CollectionTemplate<AttributeTemplate>(attributesTemplate);
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<ItemTemplate<AttributeTemplate>> Attributes { get; private set; }

    }
}
