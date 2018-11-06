using Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SwaggerAppService
    {
        private IJsonRepository _jsonRepository;

        public SwaggerAppService(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        public async Task<string> GetSwaggerJson() => await _jsonRepository.Get();

    }
}
