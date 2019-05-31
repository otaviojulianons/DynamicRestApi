using System.Text.RegularExpressions;
using Domain.ValueObjects;

namespace Application.Models
{
    public class Attribute
    {
        public string Name { get; set; }
        public EnumDataTypes DataType { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }
    }
}
