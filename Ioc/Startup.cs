using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
namespace Ioc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos.AddDbContext<AppDbContext>();
            servicos.AddScoped(typeof(ContextRepository<>));

            servicos.AddScoped(typeof(DynamicDbContext<>));
            servicos.AddScoped(typeof(DynamicRepository<>));

        }


    }
}
