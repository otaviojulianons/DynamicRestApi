using Domain.ValueObjects;

namespace Application.Commands
{
    public class ElementDto
    {
        public EnumDataTypes DataType { get; set; }
        public CreateEntityCommand Entity { get; set; }
    }
}
