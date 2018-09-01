using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
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
        private LanguageService _languageService;
        private AppDbContext _dbContext;

        public EntityService(
            IServiceProvider serviceProvider,
            ContextRepository<EntityDomain> entityRepository,
            ContextRepository<AttributeDomain> attributeRepository,
            ContextRepository<DataTypeDomain> dataTypeRepository,
            AppDbContext dbContext,
            DynamicService dynamicService,
            LanguageService languageService
            )
        {
            _serviceProvider = serviceProvider;
            _entityRepository = entityRepository;
            _attributeRepository = attributeRepository;
            _dataTypeRepository = dataTypeRepository;
            _dynamicService = dynamicService;
            _languageService = languageService;
            _dbContext = dbContext;
        }

        public List<EntityDomain> GetAllEntities()
        {
            return _entityRepository.QueryBy()
                    .Include(x => x.Attributes)
                    .ThenInclude(x => x.DataType).ToList();
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
            _dbContext.Drop(entity.Name);
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
