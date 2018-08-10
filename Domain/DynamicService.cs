using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Domain
{
    public class DynamicService
    {
        public static string GetCode(string name)
        {
            var code = new StringBuilder();
            code.AppendLine("using System;");
            code.AppendLine("using SharedKernel.Repository;");
            code.AppendLine($"public class {name}: IEntity");
            code.AppendLine("{");
            code.AppendLine(" public long Id { get; set; }");
            code.AppendLine(" public string Title { get; set; }");
            code.AppendLine(" public string Author { get; set; }");
            code.AppendLine("}");
            return code.ToString();
        }

        public static dynamic GenerateTypeFromCode(string code)
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
