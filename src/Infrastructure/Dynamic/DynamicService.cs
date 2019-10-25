using Domain.Entities.EntityAggregate;
using Domain.Services;
using Infrastructure.Templates;
using Infrastructure.WebSockets;
using InfrastructureTypes.Factories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Dynamic
{
    public class DynamicService : IDynamicDomainService
    {
        private WebSocketService _webSocketService;
        private DynamicRoutesService _dynamicRoutes;
        private List<Type> _dynamicTypes = new List<Type>();
        private string _templateDomain;
        private readonly ILogger _logger;

        public DynamicService(
            ILogger<DynamicService> logger,
            DynamicRoutesService dynamicRoutes,
            WebSocketService webSocketManager
            )
        {
            _webSocketService = webSocketManager;
            _dynamicRoutes = dynamicRoutes;
            _logger = logger;

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, $"Templates{Path.DirectorySeparatorChar}Template.Domain.mustache")))
                _templateDomain = reader.ReadToEnd();
        }

        public IEnumerable<Type> DynamicTypes => _dynamicTypes;

        public void GenerateType(params EntityDomain[] entities)
        {
            var factoryCSharpDataType = new CSharpDataTypeFactory();
            var entitiesTemplates =
                entities.Select(entity => new EntityTemplate(entity, factoryCSharpDataType));

            var classCode = new List<string>();
            foreach (var entityTemplate in entitiesTemplates)
                classCode.Add(TemplateService.Generate(_templateDomain, entityTemplate));

            Assembly dynamicAssembly = CompilerService.GenerateAssemblyFromCode(classCode.ToArray());
            foreach (var entity in entitiesTemplates)
            {
                var type = dynamicAssembly.GetType("DynamicAssembly." + entity.Name);
                _dynamicTypes.Add(type);
                _logger.LogInformation($"Type {entity.Name} generated.");

                _dynamicRoutes.AddRoute(entity.Name, type);
                _logger.LogInformation($"Route {entity.Name} generated.");

                _webSocketService.AddChannel($"/{entity.Name}/Subscribe", type);
                _logger.LogInformation($"Channel {entity.Name} generated.");
            }
        }

        public void RemoveType(params EntityDomain[] entities)
        {
            foreach (var entity in entities)
            {
                _dynamicTypes.RemoveAll(t => t.Name == entity.Name);
                _logger.LogInformation($"Type {entity.Name} removed.");

                _webSocketService.RemoveChannel(entity.Name);
                _logger.LogInformation($"Channel {entity.Name} removed.");
            }
        }
    }
}
