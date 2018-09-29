using Domain.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        private IDynamicService _service;

        public SwaggerController(IDynamicService service)
        {
            _service = service;
        }

        [HttpGet]
        public object Get()
        {
            return _service.GetSwaggerJson();
        }
    }
}