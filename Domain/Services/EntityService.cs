using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class EntityService
    {
        private IServiceProvider _serviceProvider;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<AttributeDomain> _attributeRepository;
        private IRepository<DataTypeDomain> _dataTypeRepository;

        private IDynamicService _dynamicService;
        private IDatabaseService _databaseService;
        private LanguageService _languageService;

        public EntityService(
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IRepository<AttributeDomain> attributeRepository,
            IRepository<DataTypeDomain> dataTypeRepository,
            IDatabaseService databaseService,
            IDynamicService dynamicService,
            LanguageService languageService
            )
        {
            _serviceProvider = serviceProvider;
            _entityRepository = entityRepository;
            _attributeRepository = attributeRepository;
            _dataTypeRepository = dataTypeRepository;
            _databaseService = databaseService;
            _dynamicService = dynamicService;
            _languageService = languageService;
        }

        public List<EntityDomain> GetAllEntities()
        {
            return _entityRepository.QueryBy()
                    .Include(x => x.Attributes)
                    .ThenInclude(x => x.DataType).ToList();
        }

        public void Insert(EntityDomain entity)
        {
            var validationEntity = new EntityValidation().Validate(entity);

            if (!validationEntity.IsValid)
                throw new Exception(validationEntity.Errors.First().ErrorMessage);

            var dataTypes = _dataTypeRepository.GetAll();
            entity.Attributes.ForEach(attribute =>
            {
                attribute.DataTypeId = dataTypes.FirstOrDefault(type => type.Name == attribute.DataTypeName)?.Id ?? 0;
            });
            _entityRepository.Insert(entity);

            var attributeValidator = new AttributeValidation();
            entity.Attributes.ForEach(attribute =>
            {
                var validationAttribute = attributeValidator.Validate(attribute);
                if (!validationAttribute.IsValid)
                    throw new Exception(validationAttribute.Errors.First().ErrorMessage);

                attribute.EntityId = entity.Id;
                _attributeRepository.Insert(attribute);
            });
            _entityRepository.Commit();

            var entities = GetAllEntities();
            var languageCsharp = _languageService.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            var languageSwagger = _languageService.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);

            LoadLanguage(entity, languageCsharp);
            _dynamicService.GenerateControllerDynamic(_serviceProvider, new List<EntityDomain>(){ entity });

            LoadLanguage(entity, languageSwagger);
            _dynamicService.GenerateSwaggerFile(entities);
        }

        public void Delete(long id)
        {
            var entity = _entityRepository.QueryById(id).Include(x => x.Attributes).FirstOrDefault();
            if (entity == null)
                throw new Exception("Entity not found.");

            entity.Attributes.ForEach(attribute =>
            {
                _attributeRepository.Delete(attribute.Id);
            });
            _entityRepository.Delete(entity.Id);
            _entityRepository.Commit();
            _databaseService.DropEntity(entity.Name);
        }

        public void LoadLanguage(List<EntityDomain> entities, LanguageDomain language)
        {
            entities.ForEach(entity =>
            {
                LoadLanguage(entity, language);
            });
        }

        public void LoadLanguage(EntityDomain entity, LanguageDomain language)
        {
            entity.Attributes.ForEach(attribute =>
            {
                attribute.TypeLanguage = _languageService.GetTypeLanguage(
                    language,
                    attribute.DataTypeId,
                    attribute.AllowNull);
            });
        }

    }
}
