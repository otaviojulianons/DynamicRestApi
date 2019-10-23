using Domain.Interfaces.Infrastructure;
using Infrastructure.Repository.Contexts;

namespace Infrastructure.Services
{
    public class DatabaseService : IDatabaseService
    {
        private ContextMongodb _context;

        public DatabaseService(ContextMongodb context)
        {
            _context = context;
        }

        public void DropEntity(string name) => _context.Database.DropCollection(name);

    }
}
