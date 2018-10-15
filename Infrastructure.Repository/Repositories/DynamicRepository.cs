using Domain.Core.Interfaces.Structure;
using Infrastructure.Repositories;
using Infrastructure.Repository.Contexts;
using MediatR;
using SharedKernel.Messaging;

namespace Infrastructure.Repository.Repositories
{
    public class DynamicRepository<T> : Repository<T> where T : class, IEntity
    {
        public DynamicDbContext<T> Context { get; set; }

        public DynamicRepository(DynamicDbContext<T> context, IMediator mediator, IMsgManager msg) : base(context, mediator, msg)
        {
            Context = context;
        }
    }
}
