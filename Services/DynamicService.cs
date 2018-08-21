
using Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.Extensions.DependencyInjection;
using Mustache;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.Emit;

namespace Services
{
    public class DynamicService
    {

        private DynamicRoutesCollection _dynamicRoutes;
        private string _templateDomain;
        private string _templateSwagger;

        public DynamicService(
            IServiceProvider serviceProvider,
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
                var entityRepository = scope.ServiceProvider.GetRequiredService<EntityService>();
                LoadAllEntities(provider, entityRepository.GetAllEntities());

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

        public void LoadAllEntities(IServiceProvider serviceProvider, IEnumerable<EntityDomain> entities)
        {
            foreach (var entity in entities)
                GenerateControllerDynamic(serviceProvider,entity);
            GenerateSwaggerFile(entities.FirstOrDefault());
        }



        public void GenerateControllerDynamic(IServiceProvider serviceProvider,EntityDomain entity)
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

        public string GenerateSwaggerFileFromEntity(EntityDomain entity)
        {
            var id = entity.Attributes.Find(x => x.Name.ToLower() == "id");
            entity.Attributes.Remove(id);
            entity.Attributes.FirstOrDefault().First = true;
            entity.Attributes.LastOrDefault().Last = true;


            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(_templateSwagger);
            var code = generator.Render(entity);

            entity.Attributes.Insert(0, id);
            return code;
        }

        public void GenerateSwaggerFile(EntityDomain entity)
        {
            var path = "swaggerDynamic.json";
            var swagger = GenerateSwaggerFileFromEntity(entity);
            File.Delete(path);
            using (StreamWriter writer = new StreamWriter(path))
                writer.WriteLine(swagger);
        }

        public dynamic GenerateTypeFromCode(string code)
        {
            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithDocumentationMode(DocumentationMode.Parse)
                .WithKind(SourceCodeKind.Regular) // ...as representing a complete .cs file
                .WithLanguageVersion(LanguageVersion.Latest);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(@code, parseOptions);
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
                var emitResult = compilation.Emit(ms,xmlDocumentationStream: xmlStream,options: options);
                if (!emitResult.Success)
                    throw new Exception("Compile code error");

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                return assembly.GetExportedTypes().FirstOrDefault();
            }
        }

    }
}
