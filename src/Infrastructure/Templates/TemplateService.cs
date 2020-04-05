using Mustache;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Templates
{
    public class TemplateService
    {
        public enum TemplateType
        {
            Entity,
            Controller,
            Model
        }

        private static readonly string _templatePath = 
            Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),"Templates");

        public static IEnumerable<string> LoadTemplates()
        {
            return Directory.GetFiles(_templatePath, "*.mustache")
                .Select(x => ReadTemplate(x))
                .ToList();          
        }

        private static string GetTemplatePath(TemplateType type)
        {
            var templateName = type.ToString();
            return Path.Combine(_templatePath, $"Templates{Path.DirectorySeparatorChar}.{templateName}.mustache");
        }

        private static string ReadTemplate(string templatePath)
        {
            using (StreamReader reader = new StreamReader(templatePath))
                return reader.ReadToEnd();
        }

        public static string Generate(string template, object parameters)
        {
            FormatCompiler compiler = new FormatCompiler();
            compiler.RemoveNewLines = false;
            Generator generator = compiler.Compile(template);
            return generator.Render(parameters);
        }
    }
}
