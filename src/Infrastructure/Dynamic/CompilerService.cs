using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Dynamic
{
    public class CompilerService
    {

        public static Assembly GenerateAssemblyFromCode(params string[] classCode)
        {
            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithDocumentationMode(DocumentationMode.Parse)
                .WithKind(SourceCodeKind.Regular)
                .WithLanguageVersion(LanguageVersion.Latest);
            
            EmitOptions options = new EmitOptions();

            var files = new List<SyntaxTree>();
            foreach (var @class in classCode)
                files.Add(CSharpSyntaxTree.ParseText(@class, parseOptions));

            var referencesBuild = new List<MetadataReference>();

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var path in Directory.GetFiles(assemblyFolder, "*.dll"))
            {
                var reference = MetadataReference.CreateFromFile(path);
                referencesBuild.Add(reference);
            }

            var coreDir = Directory.GetParent(typeof(Guid).GetTypeInfo().Assembly.Location);
            referencesBuild.Add(MetadataReference.CreateFromFile(Assembly.Load("netstandard").Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(IGenericRepository<,>).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(IEntity).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(INotificationManager).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(Controller).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(RouteAttribute).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(coreDir.FullName + Path.DirectorySeparatorChar + "System.Runtime.dll"));

            var compilation = CSharpCompilation.Create("DynamicAssembly", 
                              files,
                              referencesBuild,
                              new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var xmlStream = new MemoryStream();
                var emitResult = compilation.Emit(ms, xmlDocumentationStream: xmlStream, options: options);
                if (!emitResult.Success)
                    throw new Exception("Compile code error");

                ms.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(ms.ToArray());
            }
        }
    }
}
