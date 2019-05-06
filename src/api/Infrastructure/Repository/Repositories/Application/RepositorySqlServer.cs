using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Infrastructure.Data.Repository.Contexts;
using Infrastructure.Data.Repository.Repositories.Bases;
using MediatR;

namespace Infrastructure.Data.Repository.Repositories.Application
{
    public class RepositorySqlServer<T> : BaseRepositorySqlServer<T>, IRepository<T> where T :  class, IEntity
    {

        public RepositorySqlServer(DbContextSqlServer context, IMediator mediator, INotificationManager msg) : base(context,mediator,msg)
        {
        }

    }
}
