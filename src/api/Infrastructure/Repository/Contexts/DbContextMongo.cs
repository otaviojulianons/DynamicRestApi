using Infrastructure.Data.Repository.Contexts.Base;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Data.Repository.Contexts
{
    public class DbContextMongo : MongoDbContext
    {

        public DbContextMongo(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Database:Mongodb:ConnectionString");
            var databaseName = configuration.GetValue<string>("Database:Mongodb:DatabaseName");
            var mongoClient = new MongoClient(connectionString);
            Database = mongoClient.GetDatabase(databaseName);
        }

    }
}
