using MongoDB.Driver;

namespace Infrastructure.Data.Repository.Contexts
{
    public abstract class ContextMongodbAbstract
    {
        public IMongoDatabase Database { get; protected set; }
    }
}
