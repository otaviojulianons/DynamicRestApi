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
                try
                {
                    Type type = _dynamicRoutes.Get(route);
                    var controllerType = typeof(DynamicController<>).MakeGenericType(type);
                    var storageType = typeof(DynamicRepository<>).MakeGenericType(type);
                    dynamic serviceController = httpContext.RequestServices.GetService(controllerType);

                    var method = httpContext.Request.Method;
                    long? id = _dynamicRoutes.GetIdRoute(route);
                    object model = null;
                    dynamic result = null;

                    if (method == "PUT" || method == "DELETE")
                    {
                        if (!id.HasValue)
                            throw new Exception("Invalid url param.");
                    }

                    if (method == "PUT" || method == "POST")
                    {
                        using (Stream receiveStream = httpContext.Request.Body)
                        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            model = JsonConvert.DeserializeObject(readStream.ReadToEnd(), type);
                    }

                    switch (httpContext.Request.Method)
                    {
                        case "GET":
                            result = id.HasValue ? serviceController.Get((long)id) : serviceController.List();
                            break;
                        case "POST":
                            result = serviceController.Post(model);
                            break;
                        case "PUT":
                            result = serviceController.Put((long)id, model);
                            break;
                        case "DELETE":
                            result = serviceController.Delete((long)id);
                            break;
                        default:
                            return _next(httpContext);
                    }

                    string json = JsonConvert.SerializeObject(result);
                    return httpContext.Response.WriteAsync(json);
                }
                catch (Exception ex)
                {
                    httpContext.Response.StatusCode = 400;
                    return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { ex.Message }));
                }
            }
            else
                return _next(httpContext);
        }

    }
}


