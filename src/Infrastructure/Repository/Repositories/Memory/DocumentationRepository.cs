using Domain.Interfaces.Infrastructure;
using Newtonsoft.Json;

namespace Infrastructure.Data.Repository.Repositories.Memory
{
    public class DocumentationRepository : IDocumentationRepository
    {
        private object _swaggerObject;

        public object Get() => _swaggerObject;

        public void Update(string json) => _swaggerObject = JsonConvert.DeserializeObject(json);

    }
}