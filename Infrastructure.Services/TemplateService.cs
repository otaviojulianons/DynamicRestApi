using Mustache;

namespace Infrastructure.Services
{
    class TemplateService
    {
        public static string Generate(string template, object parameters)
        {
            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(template);
            var code = generator.Render(parameters);
            return code;
        }
    }
}
