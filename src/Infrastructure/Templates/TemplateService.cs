using Mustache;
using System.IO;
using System.Reflection;

namespace Infrastructure.Templates
{
    public class TemplateService
    {
        public enum TemplateType
        {
            Entity,
            Controller,
            Swagger
        }

        public static string LoadTemplate(TemplateType type)
        {
            var templateName = type.ToString();
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader(Path.Combine(path, $".{Path.DirectorySeparatorChar}Template.{templateName}.mustache")))
                return reader.ReadToEnd();
        }

        public static string Generate(string template, object parameters)
        {
            FormatCompiler compiler = new FormatCompiler();
            compiler.RemoveNewLines = false;
            Generator generator = compiler.Compile(template);
            var code = generator.Render(parameters);
            return code;
        }
    }
}
