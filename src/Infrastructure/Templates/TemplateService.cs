using Mustache;

namespace Infrastructure.Templates
{
    class TemplateService
    {
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
