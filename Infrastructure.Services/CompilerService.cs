using Domain.Core.Interfaces.Structure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Services
{
    class CompilerService
    {
        public static dynamic GenerateTypeFromCode(string classCode)
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
                                  MetadataReference.CreateFromFile(typeof(IEntity).Assembly.Location),
                              },
                              new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            //MetadataReference.CreateFromFile(typeof(IMediator).Assembly.Location),     
            //MetadataReference.CreateFromFile(Assembly.Load("System.Runtime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location),
            //MetadataReference.CreateFromFile(Assembly.Load("System.ComponentModel.Annotations, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a").Location),
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
    }
}
