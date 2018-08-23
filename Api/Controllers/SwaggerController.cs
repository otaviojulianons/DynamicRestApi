using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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