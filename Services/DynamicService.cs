
using Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mustache;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Services
{
    public class DynamicService
    {

        private DynamicRoutesCollection _dynamicRoutes;
        private string _template;

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
                _template = reader.ReadToEnd();
        }

        public void LoadAllEntities(IServiceProvider serviceProvider, IEnumerable<EntityDomain> entities)
        {
            foreach (var entity in entities)
                GenerateControllerDynamic(serviceProvider,entity);
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
            Generator generator = compiler.Compile(_template);
            var result = generator.Render(entity);
            return result;
        }

        public dynamic GenerateTypeFromCode(string code)
        {
            var compilation = CSharpCompilation.Create("DynamicAssembly", new[] { CSharpSyntaxTree.ParseText(code) },
                              new[] {
                                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                                MetadataReference.CreateFromFile(typeof(SharedKernel.Repository.IEntity).Assembly.Location),
                              },
                              new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using (var ms = new MemoryStream())
            {
                var emitResult = compilation.Emit(ms);

                if (!emitResult.Success)
                    throw new Exception("Compile code error");

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                return assembly.GetExportedTypes().FirstOrDefault();
            }
        }

  
    }
}
