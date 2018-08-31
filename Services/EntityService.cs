using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class EntityService
    {
        private IServiceProvider _serviceProvider;
        private ContextRepository<EntityDomain> _entityRepository;
        private ContextRepository<AttributeDomain> _attributeRepository;
        private ContextRepository<DataTypeDomain> _dataTypeRepository;
        private DynamicService _dynamicService;

        public EntityService(
            IServiceProvider serviceProvider,
            ContextRepository<EntityDomain> entityRepository,
            ContextRepository<AttributeDomain> attributeRepository,
            ContextRepository<DataTypeDomain> dataTypeRepository,
            DynamicService dynamicService
            )
        {
            _serviceProvider = serviceProvider;
            _entityRepository = entityRepository;
            _attributeRepository = attributeRepository;
            _dataTypeRepository = dataTypeRepository;
            _dynamicService = dynamicService;
        }

        public IEnumerable<EntityDomain> GetAllEntities()
        {
            return _entityRepository.QueryBy()
                    .Include(x => x.Attributes)
                    .ThenInclude(x => x.DataType);
        }

        public void Insert(EntityDomain entity)
        {
            entity.Validate();
            var dataTypes = _dataTypeRepository.GetAll();
            entity.Attributes.ForEach(attribute =>
            {
                attribute.DataTypeId = dataTypes.FirstOrDefault(type => type.Name == attribute.DataTypeName)?.Id ?? 0;
            });
            _entityRepository.Insert(entity);
            entity.Attributes.ForEach(attribute =>
            {
                attribute.Validate();
                attribute.EntityId = entity.Id;
                _attributeRepository.Insert(attribute);
            });
            _entityRepository.Commit();
            _dynamicService.GenerateControllerDynamic(_serviceProvider, entity);
            var entities = GetAllEntities();
            _dynamicService.GenerateSwaggerFile(entities);
        }
           
    }
}
