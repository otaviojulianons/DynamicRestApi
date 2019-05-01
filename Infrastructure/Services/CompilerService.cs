using Domain.Core.Interfaces.Structure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data.Services
{
    class CompilerService
    {
        public static (byte[] assembly, Type type) GenerateTypeFromCode(string classCode,string className,params byte[][] references)
        {
            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithDocumentationMode(DocumentationMode.Parse)
                .WithKind(SourceCodeKind.Regular) // ...as representing a complete .cs file
                .WithLanguageVersion(LanguageVersion.Latest);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(@classCode, parseOptions);
            EmitOptions options = new EmitOptions();

            var referencesBuild = new List<MetadataReference>();
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(IEntity).Assembly.Location));
            foreach(var reference in references)
            {
                var streamAssembly = new MemoryStream(reference);
                referencesBuild.Add(MetadataReference.CreateFromStream(streamAssembly));
            }

            var compilation = CSharpCompilation.Create("Dynamic" + className, new[] { tree },
                              referencesBuild,
                              new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var xmlStream = new MemoryStream();
                var emitResult = compilation.Emit(ms, xmlDocumentationStream: xmlStream, options: options);
                if (!emitResult.Success)
                    throw new Exception("Compile code error");

                ms.Seek(0, SeekOrigin.Begin);
                var byteArray = ms.ToArray();
                var assembly = Assembly.Load(byteArray);
                return (byteArray, assembly.GetExportedTypes().FirstOrDefault());
            }
        }


        public static Assembly GenerateAssemblyFromCode(params string[] classCode)
        {
            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithDocumentationMode(DocumentationMode.Parse)
                .WithKind(SourceCodeKind.Regular) // ...as representing a complete .cs file
                .WithLanguageVersion(LanguageVersion.Latest);
            
            EmitOptions options = new EmitOptions();

            var files = new List<SyntaxTree>();
            foreach (var @class in classCode)
                files.Add(CSharpSyntaxTree.ParseText(@class, parseOptions));


            var coreDir = Directory.GetParent(typeof(Guid).GetTypeInfo().Assembly.Location);
            var referencesBuild = new List<MetadataReference>();
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(Guid).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(IEntity).Assembly.Location));
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
