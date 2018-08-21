using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QSwagGenerator;
using QSwagSchema;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Api.Areas.Templates.Controllers
{
    [Route("[controller]/[action]")]
    public class GenerationController : BaseController
    {
        private DynamicService _dynamicService;
        private EntityService _entityService;

        public GenerationController(
            DynamicService dynamicService,
            EntityService entityService
            )
        {
            _dynamicService = dynamicService;
            _entityService = entityService;
        }

        [HttpGet]
        public object Generate()
        {
            var entity = _entityService.GetAllEntities().ToList().FirstOrDefault();
            var file =  _dynamicService.GenerateSwaggerFileFromEntity(entity);
           _dynamicService.GenerateSwaggerFile(entity);
            return JsonConvert.DeserializeObject(file);
        }

    }
}
