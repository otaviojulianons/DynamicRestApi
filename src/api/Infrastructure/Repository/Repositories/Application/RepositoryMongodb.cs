using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Infrastructure.CrossCutting.Notifications;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories.Bases;
using MediatR;

namespace Infrastructure.Data.Repository.Repositories.Application
{
    public class RepositoryMongodb<T> : BaseRepositoryMongodb<T>, IRepository<T> where T :  class, IEntity
    {

        public RepositoryMongodb(DbContextMongo context, IMediator mediator, INotificationManager msg) : base(context,mediator,msg)
        {
        }

    }
}
