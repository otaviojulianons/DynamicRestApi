using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.ValueObjects
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumDataTypes
    {
        Null,
        Identifier,
        String,
        Bool,
        Int,
        Long,
        Money,
        DateTime,
        Object,
        Array
    }
}