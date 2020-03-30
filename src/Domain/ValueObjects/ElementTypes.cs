using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.ValueObjects
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumElementType
    {
        Object,
        Array
    }
}
