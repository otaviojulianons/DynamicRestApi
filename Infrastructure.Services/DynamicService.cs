using Domain.Interfaces.Infrastructure;
using Domain.Models;
using Infrastructure.Repository.Contexts;
using Infrastructure.Services.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernel.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Infrastructure.Services
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
            using (StreamReader reader = new StreamReader(Path.Combine(path, $"Templates{Path.DirectorySeparatorChar}Domain.mustache")))
                _templateDomain = reader.ReadToEnd();
            using (StreamReader reader = new StreamReader(Path.Combine(path, $"Templates{Path.DirectorySeparatorChar}Swagger.mustache")))
                _templateSwagger = reader.ReadToEnd();
        }

        public void GenerateControllerDynamic(IServiceProvider serviceProvider, params EntityTemplate[] entities)
        {
            var classCode = new List<string>();
            foreach (var entity in entities)
                classCode.Add(TemplateService.Generate(_templateDomain, entity));

            Assembly dynamicAssembly = CompilerService.GenerateAssemblyFromCode(classCode.ToArray());
            foreach (var entity in entities)
            {
                //compile domain type
                //var classDomain = TemplateService.Generate(_templateDomain, entity);
                //var (assembly, type) = CompilerService.GenerateTypeFromCode(classDomain, entity.Name, _assemblies.ToArray());

                //stored assembly reference
                //_assemblies.Add(assembly);

                var type = dynamicAssembly.GetType("DynamicAssembly." + entity.Name);

                //generate controller route
                _dynamicRoutes.AddRoute(entity.Name, type);

                _webSocketService.Channels.Add($"/{entity.Name}/Subscribe",type);

                //create repository entity
                var repositoryType = typeof(DynamicDbContext<>).MakeGenericType(type);
                dynamic dynamicRepository = serviceProvider.GetService(repositoryType);
                dynamicRepository.CreateEntity();

                _logger.LogInformation($"Dynamic Controller {entity.Name} generated.");
            }
        }

        public string GenerateSwaggerJsonFile(params EntityTemplate[] entities)
        {
            var templateParameters = new { Entities = new NavigableList<EntityTemplate>(entities) };
            var json = TemplateService.Generate(_templateSwagger, templateParameters);

            _logger.LogInformation("Dynamic documentation generated.");
            return json;
        }

    }
}
