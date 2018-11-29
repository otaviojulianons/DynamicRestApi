using Application.Services;
using Domain.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        private ISwaggerRepository _swaggerRepository;

        public SwaggerController(ISwaggerRepository swaggerRepository)
        {
            _swaggerRepository = swaggerRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_swaggerRepository.GetSwaggerObject());
        }
    }
}