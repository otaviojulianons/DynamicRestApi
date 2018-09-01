﻿
using Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.DependencyInjection;
using Mustache;
using Newtonsoft.Json;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Services
{
    public class DynamicService
    {

        private DynamicRoutesCollection _dynamicRoutes;
        private string _templateDomain;
        private string _templateSwagger;

        public DynamicService(
            DynamicRoutesCollection dynamicRoutes
            )
        {
            _dynamicRoutes = dynamicRoutes;

            LoadTemplate();
        }

        public void Init(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var entityService = scope.ServiceProvider.GetRequiredService<EntityService>();
                var languageService = scope.ServiceProvider.GetRequiredService<LanguageService>();

                var languageCharp = languageService.GetById((long)LanguageDomain.EnumLanguages.Csharp);
                var languageSwagger = languageService.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);
                var entities = entityService.GetAllEntities();

                entityService.LoadLanguage(entities, languageCharp);
                GenerateControllerDynamic(provider, entities);

                entityService.LoadLanguage(entities, languageSwagger);
                GenerateSwaggerFile(entities);
            }
        }

        private void LoadTemplate()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, @"Templates\Domain.mustache")))
                _templateDomain = reader.ReadToEnd();
            using (StreamReader reader = new StreamReader(Path.Combine(path, @"Templates\Swagger.mustache")))
                _templateSwagger = reader.ReadToEnd();
        }


        public void GenerateControllerDynamic(IServiceProvider serviceProvider, List<EntityDomain> entities)
            => entities.ForEach( e => GenerateControllerDynamic(serviceProvider, e));
        //{
        //    foreach (var entity in entities)
        //        GenerateControllerDynamic(entity);
        //}

        public void GenerateControllerDynamic(IServiceProvider serviceProvider, EntityDomain entity)
        {
            var classDomain = GenerateClassDomainFromEntity(entity);
            var type = GenerateTypeFromCode(classDomain);
            _dynamicRoutes.AddRoute(entity.Name, type);
            var repositoryType = typeof(DynamicDbContext<>).MakeGenericType(type);
            dynamic dynamicRepository = serviceProvider.GetService(repositoryType);
            dynamicRepository.Create();
        }


        public string GenerateClassDomainFromEntity(EntityDomain entity)
        {
            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(_templateDomain);
            var code = generator.Render(entity);
            return code;
        }

        public dynamic GenerateTypeFromCode(string classCode)
        {
            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithDocumentationMode(DocumentationMode.Parse)
                .WithKind(SourceCodeKind.Regular) // ...as representing a complete .cs file
                .WithLanguageVersion(LanguageVersion.Latest);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(@classCode, parseOptions);
            EmitOptions options = new EmitOptions();

            var compilation = CSharpCompilation.Create("DynamicAssembly", new[] { tree },
                              new[] {
                                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                                MetadataReference.CreateFromFile(typeof(SharedKernel.Repository.IEntity).Assembly.Location),
                              },
                              new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using (var ms = new MemoryStream())
            {
                var xmlStream = new MemoryStream();
                var emitResult = compilation.Emit(ms, xmlDocumentationStream: xmlStream, options: options);
                if (!emitResult.Success)
                    throw new Exception("Compile code error");

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                return assembly.GetExportedTypes().FirstOrDefault();
            }
        }

        public string GenerateSwaggerFileFromEntity(List<EntityDomain> entities)
        {
            ConfigureNavigation(entities);
            foreach (var entity in entities)
            {
                entity.Attributes.Remove(entity.Attributes.Find(x => x.Name.ToLower() == "id"));
                ConfigureNavigation(entity.Attributes);
            }

            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(_templateSwagger);
            var code = generator.Render(new { Entities = entities });
            return code;
        }

        public void ConfigureNavigation(IEnumerable<Navigable> itens)
        {
            var first = itens.FirstOrDefault();
            if(first != null)
                first.First = true;
            var last = itens.LastOrDefault();
            if (last != null)
                last.Last = true;

        }

        public void GenerateSwaggerFile(List<EntityDomain> entities)
        {
            var path = "swaggerDynamic.json";
            var swagger = GenerateSwaggerFileFromEntity(entities);
            File.Delete(path);
            using (StreamWriter writer = new StreamWriter(path))
                writer.WriteLine(swagger);
        }

        public object GetJsonSwagger()
        {
            string json = "";
            using (StreamReader reader = new StreamReader("swaggerDynamic.json"))
                json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject(json);
        }

       

    }
}
