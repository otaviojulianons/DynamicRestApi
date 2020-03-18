using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using Infrastructure.Templates;
using InfrastructureTypes.Factories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Swagger
{
    public class SwaggerService : IDocumentationDomainService
    {
        private ILogger<SwaggerService> _logger;
        private SwaggerRepository _swaggerRepository;
        private string _templateSwagger;

        public SwaggerService(
            ILogger<SwaggerService> logger,
            SwaggerRepository swaggerRepository)
        {
            _logger = logger;
            _swaggerRepository = swaggerRepository;

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, $"Templates{Path.DirectorySeparatorChar}Template.Swagger.mustache")))
                _templateSwagger = reader.ReadToEnd();
        }

        public void GenerateDocumentation(IEnumerable<EntityDomain> entities)
        {
            var swaggerDataTypeFactory = new SwaggerDataTypeFactory();
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, swaggerDataTypeFactory)).ToArray();

            var templateParameters = new { Entities = new CollectionTemplate<EntityTemplate>(entitiesTemplates) };
            var json = TemplateService.Generate(_templateSwagger, templateParameters);

            _swaggerRepository.Update(json);
            _logger.LogInformation("Dynamic documentation generated.");
        }

    }
}
