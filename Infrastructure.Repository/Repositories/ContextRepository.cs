using Domain.Interfaces.Structure;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Contexts;

namespace Infrastructure.Repository.Repositories
{
    public class ContextRepository<T> : Repository<T>, IRepository<T> where T : class, IEntity
    {
        public ContextRepository(AppDbContext contexto) : base(contexto)
        {
        }
    }
}
