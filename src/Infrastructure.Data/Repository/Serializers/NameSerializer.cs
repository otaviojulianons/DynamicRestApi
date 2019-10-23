
using Domain.Core.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Data.Repository.Serializers
{
    public  class NameSerializer : SerializerBase<Name>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Name value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public override Name Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new Name(context.Reader.ReadString());
        }        
    }
}
