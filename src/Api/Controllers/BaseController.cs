using Api.Models;
using Common.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/")]
    public class BaseController : Controller
    {
        private INotificationManager _msgs;

        public BaseController(
            INotificationManager msgs
            )
        {
            _msgs = msgs;
        }

        internal ResultApi<T> FormatResult<T>(T result)
        {
            if (_msgs.HasError)
                return FormatError<T>(_msgs.Errors.FirstOrDefault().Message);

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
