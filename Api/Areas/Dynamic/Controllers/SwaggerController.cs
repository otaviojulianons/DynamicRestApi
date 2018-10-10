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
        private DynamicAppService _service;

        public SwaggerController(DynamicAppService service)
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