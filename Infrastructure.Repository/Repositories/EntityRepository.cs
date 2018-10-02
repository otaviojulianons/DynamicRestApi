using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Structure;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class EntityRepository : Repository<EntityDomain>, IRepository<EntityDomain>
    {
        public EntityRepository(AppDbContext context, IMediator mediator) : base(context,mediator)
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
