using Common.Extensions;
using Domain.Core.Interfaces.Structure;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using Infrastructure.Templates;
using InfrastructureTypes.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Dynamic
{
    public class DynamicService : IDynamicDomainService
    {
        private readonly ILogger _logger;
        private readonly ApplicationPartManager _partManager;

        public IEnumerable<Type> Controllers { get; private set; }
        public IEnumerable<Type> Entities { get; private set; }

        public DynamicService(
            ILogger<DynamicService> logger,
            ApplicationPartManager partManager
            )
        {
            _logger = logger;
            _partManager = partManager;
        }

        public void GenerateTypes(params EntityDomain[] entities)
        {
            var factoryCSharpDataType = new CSharpDataTypeFactory();
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, factoryCSharpDataType));
            var templates = TemplateService.LoadTemplates();

            var classCode = new List<string>();
            foreach (var template in templates)
                foreach (var entityTemplate in entitiesTemplates)
                    classCode.Add(TemplateService.Generate(template, entityTemplate));

            Assembly dynamicAssembly = CompilerService.GenerateAssemblyFromCode(_logger, classCode.ToArray());

            var types = dynamicAssembly.GetTypes();
            Controllers = types.Where(type => type.BaseType == typeof(Controller));
            Entities = types.Where(type => type.ImplementsGericType(typeof(IGenericEntity<>)));

            _partManager.ApplicationParts.Add(new AssemblyPart(dynamicAssembly));
        }


    }
}
