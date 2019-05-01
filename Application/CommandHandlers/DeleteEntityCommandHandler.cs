using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class DeleteEntityCommandHandler : IRequestHandler<DeleteEntityCommand, bool>
    {
        private IRepository<EntityDomain> _entityRepository;

        public DeleteEntityCommandHandler(IRepository<EntityDomain> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public Task<bool> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
        {
            _entityRepository.Delete(request.Id);
            return Task.FromResult(true);
        }
    }
}
