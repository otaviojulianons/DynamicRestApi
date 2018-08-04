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
        public string Status()
        {
            return "OK";
        }
    }
}
