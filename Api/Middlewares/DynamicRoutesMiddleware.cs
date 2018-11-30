using Api.Controllers;
using Domain.Interfaces.Infrastructure;
using Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharedKernel.Messaging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Api.Models;

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


