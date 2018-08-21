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
        [HttpGet]
        public object Status()
        {
            return new { status = "OK" };
        }
    }
}
