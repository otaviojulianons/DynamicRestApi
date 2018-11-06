using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        private SwaggerAppService _service;

        public SwaggerController(SwaggerAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task Get()
        {
            await HttpContext.Response.WriteAsync(await _service.GetSwaggerJson());
        }
    }
}