using Application.Models;
using AutoMapper;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.ValueObjects;
using Domain.Entities;
using Domain.Entities.EntityAggregate;
using SharedKernel.Messaging;
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

        public void Insert(Entity entity)
        {
            var entityDomain = Mapper.Map<EntityDomain>(entity);

            entity.Attributes.ForEach(attribute =>
            {
                var dataType = _dataTypesRepository.QueryBy(x => attribute.BaseType() == x.Name.Value).FirstOrDefault();
                entityDomain.AddAttribute(
                        new Name(attribute.Name),
                        attribute.AllowNull, 
                        attribute.Length, 
                        attribute.GenericType(),
                        dataType);
            });

            _entityRepository.Insert(entityDomain);
        }

        public void Delete(long id)
        {
            _entityRepository.Delete(id);
        }

    }
}
