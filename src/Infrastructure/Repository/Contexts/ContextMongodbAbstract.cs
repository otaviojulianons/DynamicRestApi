using MongoDB.Driver;

namespace Infrastructure.Repository.Contexts
{
    public abstract class ContextMongodbAbstract
    {
        public IMongoDatabase Database { get; protected set; }
    }
}
