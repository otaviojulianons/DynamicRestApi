using Api.Controllers;
using Domain;
using Domain.Helpers.Collections;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository;
using Repository.Repositories;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            if (_dynamicRoutes.IsMatch(route))
            {
                try
                {
                    Type type = _dynamicRoutes.Get(route);
                    var controllerType = typeof(DynamicController<>).MakeGenericType(type);
                    var storageType = typeof(DynamicRepository<>).MakeGenericType(type);
                    dynamic serviceController = httpContext.RequestServices.GetService(controllerType);
                    dynamic serviceStorage = httpContext.RequestServices.GetService(storageType);
                    dynamic controller = Activator.CreateInstance(controllerType, serviceStorage);
                    long? id;
                    dynamic result;
                    switch (httpContext.Request.Method)
                    {
                        case "GET":
                            id = _dynamicRoutes.GetIdRoute(route);
                            if (!id.HasValue)
                                result = serviceController.List();
                            else
                                result = serviceController.Get((long)id);
                            break;
                        case "POST":
                        case "PUT":
                            string body;
                            using (Stream receiveStream = httpContext.Request.Body)
                            {
                                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                                {
                                    body = readStream.ReadToEnd();
                                }
                            }
                            object model = JsonConvert.DeserializeObject(body, type);

                            if (httpContext.Request.Method == "POST")
                                result = controller.Post(model);
                            else
                            {
                                id = _dynamicRoutes.GetIdRoute(route);
                                if (!id.HasValue)
                                    throw new Exception("Invalid url param.");
                                result = controller.Put((long)id, model);
                            }
                            break;
                        case "DELETE":
                            id = _dynamicRoutes.GetIdRoute(route);
                            if (!id.HasValue)
                                throw new Exception("Invalid url param.");
                            result = controller.Delete((long)id);
                            break;
                        default:
                            return _next(httpContext);

                    }

                    string json = JsonConvert.SerializeObject(result);
                    return httpContext.Response.WriteAsync(json);
                }
                catch (Exception ex)
                {
                    httpContext.Response.StatusCode = 500;
                    return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { ex.Message }));
                }
            }
            else
                return _next(httpContext);
        }

    }
}


