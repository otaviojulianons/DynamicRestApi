using Domain.Interfaces.Structure;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Contexts;

namespace Infrastructure.Repository.Repositories
{
    public class DynamicRepository<T> : Repository<T> where T : class, IEntity
    {
        public DynamicDbContext<T> Context { get; set; }

        public DynamicRepository(DynamicDbContext<T> context) : base(context)
        {
            Context = context;
        }
    }
}
