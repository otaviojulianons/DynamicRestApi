using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Dynamic
{
    public class DynamicRoutesMiddleware
    {
        private DynamicRoutesService _dynamicRoutes;
        private RequestDelegate _next;

        public DynamicRoutesMiddleware(RequestDelegate next, DynamicRoutesService dynamicRoutes)
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


