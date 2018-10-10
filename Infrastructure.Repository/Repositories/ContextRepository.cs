using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Structure;
using Infrastructure.Repository.Base;
using Infrastructure.Repository.Contexts;
using MediatR;

namespace Infrastructure.Repository.Repositories
{
    public class ContextRepository<T> : Repository<T>, IRepository<T> where T : class, IEntity
    {
        public ContextRepository(AppDbContext context, IMediator mediator) : base(context, mediator)
        {
        }
    }
}
