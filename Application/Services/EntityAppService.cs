using Application.Models;
using AutoMapper;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using Domain.Core.ValueObjects;
using SharedKernel.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class EntityAppService
    {
        private IMsgManager _msgs;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<DataTypeDomain> _dataTypesRepository;

        public EntityAppService(
            IRepository<EntityDomain> entityRepository,
            IRepository<DataTypeDomain> dataTypesRepository,
            IMsgManager msgs
            )
        {
            _msgs = msgs;
            _entityRepository = entityRepository;
            _dataTypesRepository = dataTypesRepository;
        }

        public IEnumerable<Entity> GetAll()
        {
            return Mapper.Map<IEnumerable<Entity>>(_entityRepository.GetAll());
        }

        public void Insert(Entity entity)
        {
            var entityDomain = Mapper.Map<EntityDomain>(entity);

            entity.Attributes.ForEach(attribute =>
            {
                var dataType = _dataTypesRepository.QueryBy(x => x.Name.Value == attribute.DataType).FirstOrDefault();
                entityDomain.AddAttribute(new Name(attribute.Name), attribute.AllowNull, attribute.Length, dataType);
            });

            _entityRepository.Insert(entityDomain);
        }

        public void Delete(long id)
        {
            _entityRepository.Delete(id);
        }

    }
}
