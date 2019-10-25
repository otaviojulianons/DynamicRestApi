using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Swagger
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        private SwaggerRepository _documentationRepository;

        public SwaggerController(SwaggerRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_documentationRepository.Get());
        }
    }
}