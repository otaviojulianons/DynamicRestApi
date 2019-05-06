using MongoDB.Driver;

namespace Infrastructure.Data.Repository.Contexts.Base
{
    public abstract class MongoDbContext
    {
        public IMongoDatabase Database { get; protected set; }
    }
}
