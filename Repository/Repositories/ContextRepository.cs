using Domain.Interfaces.Structure;
using Repository.Base;
using Repository.Contexts;

namespace Repository.Repositories
{
    public class ContextRepository<T> : Repository<T>, IRepository<T> where T : class, IEntity
    {
        public ContextRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
