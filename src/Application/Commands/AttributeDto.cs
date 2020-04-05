using Domain.ValueObjects;

namespace Application.Commands
{
    public class AttributeDto
    {
        public string Name { get; set; }
        public EnumDataTypes DataType { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }
    }
}
