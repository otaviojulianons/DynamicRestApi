using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;

namespace Api.Controllers
{
    [Route("/")]
    [Produces("application/json")]
    public class AboutController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public object About()
        {
            return new
            {
                Application = "Dyra",
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            };
        }
    }
}
