using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories.Bases;
using MediatR;
using System.Linq;
using MongoDB.Driver;

namespace Infrastructure.Data.Repository.Repositories.Application
{
    public class EntityRepositoryMongodb : BaseRepositoryMongodb<EntityDomain>, IRepository<EntityDomain>
    {
        public EntityRepositoryMongodb(ContextMongodb context, IMediator mediator, INotificationManager msg) : base(context,mediator, msg)
        {
        }

        public override IQueryable<EntityDomain> Queryble()
        {
            return _collection.AsQueryable<EntityDomain>().OrderBy( x => x.Name);
        }
    }
}
