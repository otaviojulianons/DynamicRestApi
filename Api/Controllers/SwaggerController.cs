using Domain.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        private IDocumentationRepository _documentationRepository;

        public SwaggerController(IDocumentationRepository documentationRepository)
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