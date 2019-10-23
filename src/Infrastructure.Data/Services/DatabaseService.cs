using Domain.Interfaces.Infrastructure;
using Infrastructure.Data.Repository.Contexts;

namespace Infrastructure.Data.Services
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
