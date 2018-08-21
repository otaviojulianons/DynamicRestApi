using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/Dynamic/swagger")]
    public class SwaggerController : Controller
    {
        [HttpGet]
        public object Get()
        {
            string json = "";
            using (StreamReader reader = new StreamReader("swaggerDynamic.json"))
                json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject(json);
        }
    }
}