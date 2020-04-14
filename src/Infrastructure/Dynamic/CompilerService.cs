using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Dynamic
{
    public class CompilerService
    {
        public static Regex regexOperatorAnd = new Regex("and", RegexOptions.IgnoreCase);
        public static Regex regexOperatorOr = new Regex("or", RegexOptions.IgnoreCase);

        public static Assembly GenerateAssemblyFromCode(ILogger logger, params string[] classCode)
        {
            var dynamicAssemblyName = "DynamicAssembly";
            var dynamicAssemblyPath = $"{dynamicAssemblyName}.dll";

            CSharpParseOptions parseOptions = new CSharpParseOptions()
                .WithDocumentationMode(DocumentationMode.Parse)
                .WithKind(SourceCodeKind.Regular)
                .WithLanguageVersion(LanguageVersion.Latest);
            var files = classCode.Select(@class => CSharpSyntaxTree.ParseText(@class, parseOptions)).ToList();

            var referencesBuild = new List<MetadataReference>();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var path in Directory.GetFiles(assemblyFolder, "*.dll"))
            {
                var reference = MetadataReference.CreateFromFile(path);
                referencesBuild.Add(reference);
            }

            var coreDir = Directory.GetParent(typeof(Guid).Assembly.Location).FullName;
            var netstandardLocation = Assembly.Load("netstandard").Location;

            logger.LogInformation("Core Location: " + coreDir);
            logger.LogInformation("Netstandard Location: " + netstandardLocation);

            referencesBuild.Add(MetadataReference.CreateFromFile(GetPathAssemblyFromNamespace(coreDir, "System.Runtime")));
            referencesBuild.Add(MetadataReference.CreateFromFile(netstandardLocation));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(Controller).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(RouteAttribute).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(IQueryable).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(typeof(HttpResponse).Assembly.Location));
            referencesBuild.Add(MetadataReference.CreateFromFile(GetPathAssemblyFromType(coreDir, typeof(object))));
            referencesBuild.Add(MetadataReference.CreateFromFile(GetPathAssemblyFromType(coreDir, typeof(Enumerable))));
            referencesBuild.Add(MetadataReference.CreateFromFile(GetPathAssemblyFromType(coreDir, typeof(Task))));

            var compilation = CSharpCompilation.Create(dynamicAssemblyName,
                              files,
                              referencesBuild,
                              new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var emitResult = compilation.Emit(dynamicAssemblyPath);

            if (!emitResult.Success)
            {
                foreach (var diagnostic in emitResult.Diagnostics)
                    logger.LogWarning(diagnostic.ToString());

                throw new Exception("Compile code error");
            }

            return Assembly.LoadFrom(dynamicAssemblyPath);
        }

        private static string GetPathAssemblyFromType(string coreLocation, Type type) =>
            GetPathAssemblyFromNamespace(coreLocation, type.Namespace);

        private static string GetPathAssemblyFromNamespace(string coreLocation, string @namespace) =>
            $"{coreLocation}{Path.DirectorySeparatorChar}{@namespace}.dll";

        public static Task<Func<T, bool>> CompileWhere<T>(string where)
        {
            Type type = typeof(T);

            where = regexOperatorAnd.Replace(where, "&&");
            where = regexOperatorOr.Replace(where, "||");

            var options = ScriptOptions.Default
                .AddReferences(type.Assembly, typeof(Enumerable).Assembly)
                .AddImports("System", "System.Linq");

            return CSharpScript.EvaluateAsync<Func<T, bool>>(where, options);
        }

    }
}
