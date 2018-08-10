using Api.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Areas.Templates.Controllers
{
    [Route("[controller]")]
    public class GenerationController : BaseController
    {
        private DynamicRoutesCollection _dynamicRoutes;

        public GenerationController(DynamicRoutesCollection dynamicRoutes)
        {
            _dynamicRoutes = dynamicRoutes;
        }

        [HttpGet]
        public string Generate()
        {
            string name = "book";
            var code = DynamicService.GetCode(name);
            var type = DynamicService.GenerateTypeFromCode(code);
            _dynamicRoutes.AddRoute(name,type);
            var repositoryType = typeof(DynamicDbContext<>).MakeGenericType(type);
            dynamic context = HttpContext.RequestServices.GetService(repositoryType);
            context.Create();
            return code;
        }
    }
}