using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using Infrastructure.Repository.Contexts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedKernel.Collections;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Services
{
    public class DynamicService : IDynamicService
    {
     
        private IDynamicRoutesService _dynamicRoutes;
        private string _templateDomain;
        private string _templateSwagger;
        private readonly ILogger _logger;

        public DynamicService(
            ILogger<DynamicService> logger,
            IDynamicRoutesService dynamicRoutes
            )
        {
            _dynamicRoutes = dynamicRoutes;
            _logger = logger;

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, @"Templates\Domain.mustache")))
                _templateDomain = reader.ReadToEnd();
            using (StreamReader reader = new StreamReader(Path.Combine(path, @"Templates\Swagger.mustache")))
                _templateSwagger = reader.ReadToEnd();
        }

        public void GenerateControllerDynamic(IServiceProvider serviceProvider, params EntityTemplate[] entities)
        {
            foreach (var entity in entities)
            {
                var classDomain = TemplateService.Generate(_templateDomain, entity);
                var type = CompilerService.GenerateTypeFromCode(classDomain);
                _dynamicRoutes.AddRoute(entity.Name, type);
                var repositoryType = typeof(DynamicDbContext<>).MakeGenericType(type);
                dynamic dynamicRepository = serviceProvider.GetService(repositoryType);
                dynamicRepository.CreateEntity();
                _logger.LogInformation($"Dynamic Controller {entity.Name} generated.");
            }
        }

        private string GenerateSwaggerJson(params EntityTemplate[] entities)
        {
            var templateParameters = new { Entities = new NavigableList<EntityTemplate>(entities) };
            return TemplateService.Generate(_templateSwagger, templateParameters);
        }


        public void GenerateSwaggerJsonFile(params EntityTemplate[] entities)
        {
            var swagger = GenerateSwaggerJson(entities);
            var path = "swaggerDynamic.json";
            File.Delete(path);
            using (StreamWriter writer = new StreamWriter(path))
                writer.WriteLine(swagger);
            _logger.LogInformation("Dynamic documentation generated.");
        }

        public object GetSwaggerJson()
        {
            string json = "";
            using (StreamReader reader = new StreamReader("swaggerDynamic.json"))
                json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject(json);
        }

    }
}
