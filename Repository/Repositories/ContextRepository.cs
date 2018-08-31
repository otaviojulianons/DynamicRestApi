using Repository.Contexts;
using SharedKernel.Repository;

namespace Repository.Repositories
{
    public class ContextRepository<T> : Repository<T> where T : class, IEntity
    {
        public ContextRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
