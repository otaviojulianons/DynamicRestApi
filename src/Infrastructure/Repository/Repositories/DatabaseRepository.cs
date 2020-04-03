using Infrastructure.Repository.Contexts;

namespace Infrastructure.Repository.Repositories
{
    public class DatabaseRepository
    {
        private MongodbContext _context;

        public DatabaseRepository(MongodbContext context)
        {
            _context = context;
        }

        public void Delete(string name) => _context.Database.DropCollection(name);

    }
}
