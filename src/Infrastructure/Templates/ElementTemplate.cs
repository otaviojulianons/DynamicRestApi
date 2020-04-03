using InfrastructureTypes;

namespace Infrastructure.Templates
{
    public class ElementTemplate
    {
        public ElementTemplate(EntityTemplate entity, IDataType dataType)
        {
            Entity = entity;
            DataType = dataType;
        }

        public IDataType DataType { get; private set; }
        public EntityTemplate Entity { get; private set; }
    }
}
