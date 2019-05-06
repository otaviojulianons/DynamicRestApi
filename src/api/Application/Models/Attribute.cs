using System.Text.RegularExpressions;

namespace Application.Models
{
    public class Attribute
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }
        public bool IsGenericType() => Regex.Match(DataType,@"(\<(\w+)\>)+").Success;
        public string BaseType() => Regex.Replace(DataType,@"(\<(\w+)\>)+","<>");
        public string GenericType() => Regex.Match(DataType,@"(\<(\w+)\>)+").Groups[2].Value;
    }
}
