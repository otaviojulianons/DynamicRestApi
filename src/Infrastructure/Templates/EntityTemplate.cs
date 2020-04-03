using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
using InfrastructureTypes;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Templates
{
    public class EntityTemplate
    {
        private List<AttributeTemplate> _attributesTemplate = new List<AttributeTemplate>();
        private List<ElementTemplate> _elementsTemplate = new List<ElementTemplate>();
        private List<ElementTemplate> _referencesTemplate = new List<ElementTemplate>();

        public EntityTemplate(EntityDomain entity, IDataTypeFactory dataTypeFactory)
            : this(entity, dataTypeFactory, null)
        {
        }

        public EntityTemplate(EntityDomain entity, IDataTypeFactory dataTypeFactory, EntityTemplate parent)
        {
            Name = entity.Name;
            DataType = CreateDataTypeName(parent?.DataType, entity.Name);
            Parent = parent;

            foreach (var attribute in entity.Attributes)
            {
                var dataType = dataTypeFactory.MakeValueDataType(attribute.DataType, attribute.AllowNull);
                if (attribute.IsIdentifier)
                {
                    IdenfierDataType = dataType;
                    IdenfierGuid = dataType.DataType == EnumDataTypes.Identifier;
                }
                else
                    _attributesTemplate.Add(new AttributeTemplate(attribute, dataType));
            }

            foreach (var element in entity.Elements)
            {
                var dataTypeParameter = CreateDataTypeName(DataType, element.Entity.Name);
                var dataType = dataTypeFactory.MakeGenericDataType(element.DataType, dataTypeParameter);
                var entityElement = new EntityTemplate(element.Entity, dataTypeFactory, this);

                var elementTemplate = new ElementTemplate(entityElement, dataType);
                _elementsTemplate.Add(elementTemplate);
                AddReference(elementTemplate);
            }

            Attributes = new CollectionTemplate<AttributeTemplate>(_attributesTemplate);
            Elements = new CollectionTemplate<ElementTemplate>(_elementsTemplate);
            References = new CollectionTemplate<ElementTemplate>(_referencesTemplate);
        }

        public string Name { get; private set; }

        public string DataType { get; private set; }

        public IDataType IdenfierDataType { get; private set; }

        public bool IdenfierGuid { get; private set; }

        public EntityTemplate Parent { get; private set; }

        public List<ItemTemplate<AttributeTemplate>> Attributes { get; private set; }

        public List<ItemTemplate<ElementTemplate>> Elements { get; private set; }

        public List<ItemTemplate<ElementTemplate>> References { get; private set; }

        private string CreateDataTypeName(string parent, string child) =>
            parent + child;

        protected List<AttributeTemplate> GetAttributes() => _attributesTemplate;

        protected List<ElementTemplate> GetElements() => _elementsTemplate;

        private void AddReference(ElementTemplate reference)
        {
            if (Parent == null)
                _referencesTemplate.Add(reference);
            else
                Parent.AddReference(reference);
        }

    }
}
