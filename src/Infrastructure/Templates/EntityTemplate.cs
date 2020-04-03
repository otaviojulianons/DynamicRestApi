using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
using InfrastructureTypes;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Templates
{
    public class EntityTemplate
    {
        public EntityTemplate(EntityDomain entity, IDataTypeFactory dataTypeFactory)
        {
            Name = entity.Name;
            var attributesTemplate = new List<AttributeTemplate>();
            foreach (var attribute in entity.Attributes)
            {
                var dataType = dataTypeFactory.Make(attribute.DataType, attribute.AllowNull);
                if (attribute.IsIdentifier)
                {
                    IdenfierDataType = dataType;
                    IdenfierGuid = dataType.DataType == EnumDataTypes.Identifier;
                }
                else
                    attributesTemplate.Add(new AttributeTemplate(attribute, dataType));
            }
            Attributes = new CollectionTemplate<AttributeTemplate>(attributesTemplate);
        }

        public string Name { get; private set; }

        public IDataType IdenfierDataType { get; private set; }

        public bool IdenfierGuid { get; private set; }

        public IReadOnlyCollection<ItemTemplate<AttributeTemplate>> Attributes { get; private set; }

    }
}
