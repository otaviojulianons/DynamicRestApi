using Newtonsoft.Json;

namespace Infrastructure.Swagger
{
    public class SwaggerRepository
    {
        private object _swaggerObject;

        public object Get() => _swaggerObject;

        public void Update(string json) => _swaggerObject = JsonConvert.DeserializeObject(json);

    }
}