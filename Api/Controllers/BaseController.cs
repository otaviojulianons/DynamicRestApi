using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/")]
    public class BaseController : ControllerBase
    {

        internal ResultApi<T> FormatResult<T>(T result)
        {
            return new ResultApi<T>()
            {
                Result = result,
                Message = "Method executed successfully."
            };
        }

        internal ResultApi<T> FormatError<T>(string message)
        {
            Response.StatusCode = 400;
            return new ResultApi<T>()
            {
                Result = default(T),
                Message = message
            };
        }


    }
}
