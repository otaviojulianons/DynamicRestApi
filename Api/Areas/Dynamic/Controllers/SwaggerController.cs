using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        private DynamicService _service;

        public SwaggerController(DynamicService service)
        {
            _service = service;
        }

        [HttpGet]
        public object Get()
        {
            return _service.GetJsonSwagger();
        }
    }
}