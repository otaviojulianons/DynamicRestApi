using Repository.Base;
using Repository.Contexts;
using SharedKernel.Repository;

namespace Repository.Repositories
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
