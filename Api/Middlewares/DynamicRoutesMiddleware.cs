using Api.Controllers;
using Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    class DynamicRoutesMiddleware
    {
        private DynamicRoutesCollection _dynamicRoutes;
        private RequestDelegate _next;

        public DynamicRoutesMiddleware(RequestDelegate next, DynamicRoutesCollection dynamicRoutes)
        {
            _dynamicRoutes = dynamicRoutes;
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var route = httpContext.Request.Path.Value;
            if (_dynamicRoutes.Routes.ContainsKey(route))
            {
                Type type = _dynamicRoutes.Routes[route];
                var controllerType = typeof(DynamicController<>).MakeGenericType(type);
                var storageType = typeof(DynamicRepository<>).MakeGenericType(type);
                dynamic serviceController = httpContext.RequestServices.GetService(controllerType);
                dynamic serviceStorage = httpContext.RequestServices.GetService(storageType);
                dynamic controller = Activator.CreateInstance(controllerType, serviceStorage);

                switch (httpContext.Request.Method)
                {
                    case "GET":
                        var result = serviceController.List();
                        string json = JsonConvert.SerializeObject(result);
                        httpContext.Response.StatusCode = 200;
                        return httpContext.Response.WriteAsync(json);
                    case "POST":
                        string body;
                        using (Stream receiveStream = httpContext.Request.Body)
                        {
                            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                body = readStream.ReadToEnd();
                            }
                        }
                        var model = JsonConvert.DeserializeObject(body, type);
                        controller.Post(model);
                        httpContext.Response.StatusCode = 200;
                        return httpContext.Response.WriteAsync("");
                    default:
                        return _next(httpContext);
                }

            }
            else
                return _next(httpContext);
        }

    }
}


