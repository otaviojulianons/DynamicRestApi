using Mustache;

namespace Services
{
    class TemplateService
    {
        public string Generate(string template, object parameters)
        {
            FormatCompiler compiler = new FormatCompiler();
            Generator generator = compiler.Compile(template);
            var code = generator.Render(parameters);
            return code;
        }
    }
}
