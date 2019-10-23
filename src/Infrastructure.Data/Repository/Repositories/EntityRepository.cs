using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Infrastructure.CrossCutting.Notifications;
using Infrastructure.Data.Repository.Contexts;
using MediatR;
using MongoDB.Driver;
using System.Linq;

namespace Infrastructure.Data.Repository.Repositories
{
    public class EntityRepository : MongodbRepository<EntityDomain>, IRepository<EntityDomain>
    {
        public EntityRepository(ContextMongodb context, IMediator mediator, INotificationManager msg) : base(context,mediator, msg)
        {
        }

        public override IQueryable<EntityDomain> Queryble()
        {
            return _collection.AsQueryable<EntityDomain>().OrderBy( x => x.Name);
        }
    }
}
