using Domain.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    class DynamicRoutesMiddleware
    {
        private IDynamicRoutesService _dynamicRoutes;
        private RequestDelegate _next;

        public DynamicRoutesMiddleware(RequestDelegate next, IDynamicRoutesService dynamicRoutes)
        {
            _dynamicRoutes = dynamicRoutes;
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var route = httpContext.Request.Path.Value;
            if (_dynamicRoutes.IsMatch(route))
            {
                Type type = _dynamicRoutes.GetRouteType(route);
                httpContext.Items.Add("DynamicType",type);
                httpContext.Request.Path = _dynamicRoutes.ReplaceRoute(route, "/DynamicEntity");
            }
                
            return _next(httpContext);
        }

    }
}


