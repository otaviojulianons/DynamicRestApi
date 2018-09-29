using Domain.Commands;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Helpers.Collections;
using Domain.Interfaces.Infrastructure;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Contexts;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class DynamicService : IDynamicService
        , INotificationHandler<GenerateDynamicDocumentationCommand>
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
            LoadTemplates();
        }

        public async Task Init(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var entityService = scope.ServiceProvider.GetRequiredService<EntityService>();
                var languageService = scope.ServiceProvider.GetRequiredService<LanguageService>();
                var mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();

                var languageCharp = languageService.GetById((long)LanguageDomain.EnumLanguages.Csharp);
                var languageSwagger = languageService.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);
                var entities = entityService.GetAllEntities();

                entityService.LoadLanguage(entities, languageCharp);
                GenerateControllerDynamic(provider, entities.ToArray());

                entityService.LoadLanguage(entities, languageSwagger);

                await mediatr.Publish(new GenerateDynamicDocumentationCommand(entities));
            }
        }

        private void LoadTemplates()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, @"Templates\Domain.mustache")))
                _templateDomain = reader.ReadToEnd();
            using (StreamReader reader = new StreamReader(Path.Combine(path, @"Templates\Swagger.mustache")))
                _templateSwagger = reader.ReadToEnd();
        }


        public void GenerateControllerDynamic(IServiceProvider serviceProvider, params EntityDomain[] entities)
        {
            foreach (var entity in entities)
            {
                var classDomain = new TemplateService().Generate(_templateDomain, entity);
                var type = new CompilerService().GenerateTypeFromCode(classDomain);
                _dynamicRoutes.AddRoute(entity.Name, type);
                var repositoryType = typeof(DynamicDbContext<>).MakeGenericType(type);
                dynamic dynamicRepository = serviceProvider.GetService(repositoryType);
                dynamicRepository.CreateEntity();
                _logger.LogInformation($"Dynamic Controller {entity.Name} generated.");
            }
        }

        public string GenerateSwaggerJson(params EntityDomain[] entities)
        {
            foreach (var entity in entities)
                entity.Attributes.Remove(entity.Attributes.Find(x => x.Name.ToLower() == "id"));

            var templateParameters = new { Entities = new NavigableList<EntityDomain>(entities) };
            return new TemplateService().Generate(_templateSwagger, templateParameters);
        }


        public void GenerateSwaggerJsonFile(params EntityDomain[] entities)
        {
            var path = "swaggerDynamic.json";
            var swagger = GenerateSwaggerJson(entities);
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

        public Task Handle(GenerateDynamicDocumentationCommand notification, CancellationToken cancellationToken)
        {
            GenerateSwaggerJsonFile(notification.Entities.ToArray());
            return Task.CompletedTask;
        }
    }
}
