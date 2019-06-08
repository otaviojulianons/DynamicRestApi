﻿using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.CrossCutting.WebSockets;
using Infrastructure.DataTypes.Factories;
using Infrastructure.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data.Services
{
    public class DynamicService : IDynamicService
    {
        private WebSocketService _webSocketService;
        private IDynamicRoutesService _dynamicRoutes;
        private string _templateDomain;
        private string _templateSwagger;
        private List<byte[]> _assemblies = new List<byte[]>();
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;


        public DynamicService(
            ILogger<DynamicService> logger,
            IDynamicRoutesService dynamicRoutes,
            WebSocketService webSocketManager
            )
        {
            _webSocketService = webSocketManager;
            _dynamicRoutes = dynamicRoutes;
            _logger = logger;

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, $"Templates{Path.DirectorySeparatorChar}Template.Domain.mustache")))
                _templateDomain = reader.ReadToEnd();
            using (StreamReader reader = new StreamReader(Path.Combine(path, $"Templates{Path.DirectorySeparatorChar}Template.Swagger.mustache")))
                _templateSwagger = reader.ReadToEnd();
        }

        public void GenerateControllerDynamic(IServiceProvider serviceProvider, IEnumerable<EntityDomain> entities)
        {
            var factoryCSharpDataType = new CSharpDataTypeFactory();
            var entitiesTemplates = 
                entities.Select(entity => new EntityTemplate(entity, factoryCSharpDataType));

            var classCode = new List<string>();
            foreach (var entity in entities)
                classCode.Add(TemplateService.Generate(_templateDomain, entity));

            Assembly dynamicAssembly = CompilerService.GenerateAssemblyFromCode(classCode.ToArray());
            foreach (var entity in entitiesTemplates)
            {
                var type = dynamicAssembly.GetType("DynamicAssembly." + entity.Name);

                GenerateEntityRoute(entity, type);

                GenerateEntityWebSocket(entity, type);

                _logger.LogInformation($"Dynamic Controller {entity.Name} generated.");
            }
        }

        private void GenerateEntityRoute(EntityTemplate entity, Type type) =>
            _dynamicRoutes.AddRoute(entity.Name, type);

        private void GenerateEntityWebSocket(EntityTemplate entity, Type type) =>
            _webSocketService.Channels.Add($"/{entity.Name}/Subscribe", type);

        public string GenerateSwaggerJsonFile(IEnumerable<EntityDomain> entities)
        {
            var swaggerDataTypeFactory = new SwaggerDataTypeFactory();
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, swaggerDataTypeFactory)).ToArray();

            var templateParameters = new { Entities = new CollectionTemplate<EntityTemplate>(entitiesTemplates) };
            var json = TemplateService.Generate(_templateSwagger, templateParameters);

            _logger.LogInformation("Dynamic documentation generated.");
            return json;
        }

    }
}