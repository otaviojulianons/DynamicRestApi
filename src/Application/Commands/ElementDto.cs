using Domain.ValueObjects;

namespace Application.Commands
{
    public class ElementDto
    {
        public EnumElementType DataType { get; set; }
        public CreateEntityCommand Entity { get; set; }
    }
}
