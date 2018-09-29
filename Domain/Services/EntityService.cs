using Domain.Commands;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class EntityService
    {
        private IServiceProvider _serviceProvider;
        private IMediator _mediator;
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
            IMediator mediator,
            LanguageService languageService
            )
        {
            _serviceProvider = serviceProvider;
            _mediator = mediator;
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
            var validationEntity = entity.Validator.Validate(entity);
            if (!validationEntity.IsValid)
                throw new Exception(validationEntity.Errors.First().ErrorMessage);

            var dataTypes = _dataTypeRepository.GetAll();
            foreach (var attribute in entity.Attributes)
                attribute.DataTypeId = dataTypes.FirstOrDefault(type => type.Name == attribute.DataTypeName)?.Id ?? 0;

            _entityRepository.Insert(entity);

            foreach (var attribute in entity.Attributes)
            {
                var validationAttribute = attribute.Validator.Validate(attribute);
                if (!validationAttribute.IsValid)
                    throw new Exception(validationAttribute.Errors.First().ErrorMessage);

                attribute.EntityId = entity.Id;
                _attributeRepository.Insert(attribute);
            }

            _entityRepository.Commit();


            var languageCsharp = _languageService.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            var languageSwagger = _languageService.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);

            LoadLanguage(entity, languageCsharp);
            _dynamicService.GenerateControllerDynamic(_serviceProvider, entity );

            LoadLanguage(entity, languageSwagger);
            _dynamicService.GenerateSwaggerJsonFile(GetAllEntities().ToArray());

            _mediator.Publish(
                new GenerateDynamicDocumentationCommand(GetAllEntities())
            );
        }

        public void Delete(long id)
        {
            var entity = _entityRepository.QueryById(id).Include(x => x.Attributes).FirstOrDefault();
            if (entity == null)
                throw new Exception("Entity not found.");


            foreach (var attribute in entity.Attributes)
                _attributeRepository.Delete(attribute.Id);

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
            foreach (var attribute in entity.Attributes)
            {
                attribute.TypeLanguage = _languageService.GetTypeLanguage(
                        language,
                        attribute.DataTypeId,
                        attribute.AllowNull);
            }
        }
    }
}
