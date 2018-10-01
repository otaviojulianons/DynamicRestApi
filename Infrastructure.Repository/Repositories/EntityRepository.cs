using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Structure;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class EntityRepository : Repository<EntityDomain>, IRepository<EntityDomain>
    {
        public EntityRepository(AppDbContext context) : base(context)
        {
        }

        public override IQueryable<EntityDomain> Queryble()
        {
            return DbSet.OrderBy(x => x.Id)
                        .Include(x => x.Attributes)
                        .ThenInclude(x => x.DataType);
        }
    }
}
