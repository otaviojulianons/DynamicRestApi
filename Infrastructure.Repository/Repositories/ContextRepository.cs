using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Infrastructure.Repositories;
using Infrastructure.Repository.Contexts;
using MediatR;
using SharedKernel.Messaging;

namespace Infrastructure.Repository.Repositories
{
    public class ContextRepository<T> : Repository<T>, IRepository<T> where T : class, IEntity
    {
        public ContextRepository(AppDbContext context, IMediator mediator, IMsgManager msg) : base(context, mediator, msg)
        {
        }
    }
}
