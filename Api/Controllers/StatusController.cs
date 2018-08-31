using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/")]
    public class StatusController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public Dictionary<string,string> Status()
        {
            return  new Dictionary<string, string>() { { "status", "OK" } };
        }
    }
}
