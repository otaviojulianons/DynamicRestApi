using Application.Models;
using AutoMapper;
using Domain.Entities.EntityAggregate;
using Domain.Events;
using Domain.Helpers.Extensions;
using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class EntityAppService
    {
        private IMsgManager _msgs;
        private IMediator _mediator;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<DataTypeDomain> _dataTypesRepository;
        private IDatabaseService _databaseService;

        public EntityAppService(
            IRepository<EntityDomain> entityRepository,
            IRepository<DataTypeDomain> dataTypesRepository,
            IDatabaseService databaseService,
            IMediator mediator,
            IMsgManager msgs
            )
        {
            _msgs = msgs;
            _mediator = mediator;
            _entityRepository = entityRepository;
            _dataTypesRepository = dataTypesRepository;
            _databaseService = databaseService;
        }

        public IEnumerable<Entity> GetAll()
        {
            return Mapper.Map<IEnumerable<Entity>>(_entityRepository.GetAll());
        }

        public void Insert(Entity entity)
        {
            var entityDomain = Mapper.Map<EntityDomain>(entity);

            entityDomain.DefineDataTypes(_dataTypesRepository.GetAll());

            if (!entityDomain.IsValid(_msgs))
                return;

            _entityRepository.Insert(entityDomain);

            _mediator.Publish(new CreateDynamicControllerEvent(entityDomain));

            _mediator.Publish(new GenerateDynamicDocumentationEvent());
        }

        public void Delete(long id)
        {
            var entity = _entityRepository.QueryById(id).FirstOrDefault();
            if (entity == null)
            {
                _msgs.Errors.Add(new Msg("Entity not found."));
                return;
            }

            _entityRepository.Delete(entity.Id);

            _databaseService.DropEntity(entity.Name);
        }

    }
}
