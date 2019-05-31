using Application.Commands;
using AutoMapper;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.ValueObjects;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
using Infrastructure.CrossCutting.Extensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand, bool>
    {
        private IRepository<EntityDomain> _entityRepository;

        public CreateEntityCommandHandler(
            IRepository<EntityDomain> entityRepository
            )
        {
            _entityRepository = entityRepository;
        }

        public Task<bool> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var entityDomain = Mapper.Map<EntityDomain>(request);

            request.Attributes.ForEach(attribute =>
            {
                var attributeDomain = new AttributeDomain(
                    new Name(attribute.Name),
                    attribute.DataType,
                    attribute.AllowNull,
                    attribute.Length
                );
                
                entityDomain.AddAttribute(attributeDomain);
            });

            _entityRepository.Insert(entityDomain);
            return Task.FromResult(true);
        }

 
    }
}
