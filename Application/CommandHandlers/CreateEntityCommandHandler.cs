using Application.Commands;
using AutoMapper;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.ValueObjects;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand, bool>
    {
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<DataTypeDomain> _dataTypeRepository;

        public CreateEntityCommandHandler(
            IRepository<EntityDomain> entityRepository,
            IRepository<DataTypeDomain> dataTypeRepository
            )
        {
            _entityRepository = entityRepository;
            _dataTypeRepository = dataTypeRepository;
        }

        public Task<bool> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var entityDomain = Mapper.Map<EntityDomain>(request.Entity);

            request.Entity.Attributes.ForEach(attribute =>
            {
                var dataType = _dataTypeRepository.QueryBy(x => attribute.BaseType() == x.Name.Value).FirstOrDefault();
                entityDomain.AddAttribute(
                        new Name(attribute.Name),
                        attribute.AllowNull,
                        attribute.Length,
                        attribute.GenericType(),
                        dataType);
            });

            _entityRepository.Insert(entityDomain);
            return Task.FromResult(true);
        }

 
    }
}
