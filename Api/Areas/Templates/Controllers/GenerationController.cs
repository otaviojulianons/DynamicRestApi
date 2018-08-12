using Api.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;
using System.Linq;

namespace Api.Areas.Templates.Controllers
{
    [Route("[controller]")]
    public class GenerationController : BaseController
    {
        private DynamicService _dynamicService;

        public GenerationController(DynamicService dynamicService)
        {
            _dynamicService = dynamicService;
        }

        [HttpGet]
        public string Generate()
        {
            
            return "Success";
        }
    }
}