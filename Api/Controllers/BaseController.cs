using Microsoft.AspNetCore.Mvc;
using SharedKernel.Aplicacao.Api;

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

        internal ResultApi<T> FormatError<T>(T result)
        {
            return new ResultApi<T>()
            {
                Result = result,
                Message = "Method not executed."
            };
        }


    }
}
