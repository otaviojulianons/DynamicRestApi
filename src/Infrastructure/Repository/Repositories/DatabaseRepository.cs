using Infrastructure.Repository.Contexts;

namespace Infrastructure.Repository.Repositories
{
    public class DatabaseRepository
    {
        private ContextMongodb _context;

        public DatabaseRepository(ContextMongodb context)
        {
            _context = context;
        }

        public void Delete(string name) => _context.Database.DropCollection(name);

    }
}
