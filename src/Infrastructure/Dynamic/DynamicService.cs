using Domain.Entities.EntityAggregate;
using Domain.Services;
using Infrastructure.Templates;
using InfrastructureTypes.Factories;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Dynamic
{
    public class DynamicService : IDynamicDomainService
    {
        private string _templateDomain;
        private string _templateController;
        private readonly ILogger _logger;
        private readonly ApplicationPartManager _partManager;

        public DynamicService(
            ILogger<DynamicService> logger,
            ApplicationPartManager partManager
            )
        {
            _logger = logger;
            _partManager = partManager;
            _templateDomain = TemplateService.LoadTemplate(TemplateService.TemplateType.Entity);
            _templateController = TemplateService.LoadTemplate(TemplateService.TemplateType.Controller);
        }

        public void GenerateTypes(params EntityDomain[] entities)
        {
            var factoryCSharpDataType = new CSharpDataTypeFactory();
            var entitiesTemplates =
                entities.Select(entity => new EntityTemplate(entity, factoryCSharpDataType));

            var classCode = new List<string>();
            foreach (var entityTemplate in entitiesTemplates)
                classCode.Add(TemplateService.Generate(_templateDomain, entityTemplate));
            foreach (var entityTemplate in entitiesTemplates)
                classCode.Add(TemplateService.Generate(_templateController, entityTemplate));

            Assembly dynamicAssembly = CompilerService.GenerateAssemblyFromCode(classCode.ToArray());

            _partManager.ApplicationParts.Add(new AssemblyPart(dynamicAssembly));
        }
    }
}
