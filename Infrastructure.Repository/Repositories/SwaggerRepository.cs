using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Infrastructure;
using Newtonsoft.Json;

namespace Infrastructure.Repository
{
    public class SwaggerRepository : ISwaggerRepository
    {
        private object _swaggerObject;

        public object GetSwaggerObject() 
            => _swaggerObject;

        public void Update(string json) 
            => _swaggerObject = JsonConvert.DeserializeObject(json);

    }
}