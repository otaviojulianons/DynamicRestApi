using System.Collections.Generic;
using Domain.Core.ValueObjects;
using Domain.Entities.EntityAggregate;
using Infrastructure.Repository.Serializers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure.Repository.Contexts
{
    public class ContextMongodb : ContextMongodbAbstract
    {
        public ContextMongodb(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Database:Mongodb:ConnectionString");
            var databaseName = configuration.GetValue<string>("Database:Mongodb:DatabaseName");
            var mongoClient = new MongoClient(connectionString);
            Database = mongoClient.GetDatabase(databaseName);


            if (!BsonClassMap.IsClassMapRegistered(typeof(EntityDomain))) {

                BsonClassMap.RegisterClassMap<EntityDomain>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id).SetIdGenerator(GuidGenerator.Instance);
                    cm.GetMemberMap(x => x.Name).SetSerializer(new NameSerializer());
                    cm.MapField("_attributes").SetElementName("Attributes");
                });

                BsonClassMap.RegisterClassMap<AttributeDomain>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id).SetIdGenerator(GuidGenerator.Instance);
                    cm.GetMemberMap(x => x.Name).SetSerializer(new NameSerializer());
                });              
            }
        }

    }
}
