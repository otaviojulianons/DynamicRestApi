using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Infrastructure.Repositories;
using Infrastructure.Repository.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Messaging;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class EntityRepository : Repository<EntityDomain>, IRepository<EntityDomain>
    {
        public EntityRepository(AppDbContext context, IMediator mediator, IMsgManager msg) : base(context,mediator, msg)
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
