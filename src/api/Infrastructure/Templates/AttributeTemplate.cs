using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;

namespace Infrastructure.Templates
{
    public class AttributeTemplate
    {
        public AttributeTemplate(
            AttributeDomain attribute,
            IDataType dataType
            )
        {
            Name = attribute.Name;
            Length = attribute.Length;
            AllowNull = attribute.AllowNull;
            DataType = dataType;
        }

        public string Name { get; private set; }

        public int? Length { get; private set; }

        public bool AllowNull { get; private set; }

        public IDataType DataType { get; private set; }
    }
}
