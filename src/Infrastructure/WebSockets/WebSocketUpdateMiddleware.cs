using Domain.Core.Interfaces.Infrastructure;
using Infrastructure.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.WebSockets
{
    public class WebSocketUpdateMiddleware
    {

        private RequestDelegate _next;
        private WebSocketService _webSocketManager;
        private DynamicRoutesService _dynamicRoutes;
        private readonly List<string>_allowMethods;

        public WebSocketUpdateMiddleware(
            RequestDelegate next, 
            IActionDescriptorCollectionProvider routes,
            WebSocketService webSocketManager,
            DynamicRoutesService dynamicRoutes)
        {
            _next = next;
            _webSocketManager = webSocketManager;
            _dynamicRoutes = dynamicRoutes;
            _allowMethods = new List<string>() { "PUT", "POST", "DELETE" };
        }

        public Task Invoke(HttpContext httpContext)
        {
            var route = httpContext.Request.Path.Value;
            var channel = _dynamicRoutes.FindRoute(route) + "/Subscribe";
            var isMatch = _allowMethods.Contains(httpContext.Request.Method) && _dynamicRoutes.IsMatch(route);

            var result = _next(httpContext);

            if (isMatch && httpContext.Response.StatusCode == 200)
            {
                var type = _dynamicRoutes.GetRouteType(route);
                var repositoryType = typeof(IRepository<>).MakeGenericType(type);
                dynamic repository = httpContext.RequestServices.GetService(repositoryType);
                _webSocketManager.SendAll(channel,(object)repository.GetAll());
            }

            return result;
        }



    }
}
