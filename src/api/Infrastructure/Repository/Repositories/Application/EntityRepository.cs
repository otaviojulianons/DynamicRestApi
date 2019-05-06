using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data.Repository.Repositories.Application
{
    public class EntityRepository : BaseRepositorySqlServer<EntityDomain>, IRepository<EntityDomain>
    {
        public EntityRepository(DbContextSqlServer context, IMediator mediator, INotificationManager msg) : base(context,mediator, msg)
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
