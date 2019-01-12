using Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services
{
    public class DynamicRoutesService : IDynamicRoutesService
    {
        private Dictionary<string, Type> _routes = new Dictionary<string, Type>();  

        public void AddRoute(string route,Type type)
        {
            if (!route.StartsWith("/"))
                route = $"/{route}";

            _routes.TryAdd(route, type);
        }

        public string GetRoute(string url)
            => _routes.Keys.Where(key => key == url || url.StartsWith(key + "/")).FirstOrDefault();
        
        public bool IsMatch(string url) => FindRoute(url) != null;

        public string FindRoute(string url)
            => _routes.Keys.Where(key => key == url || url.StartsWith(key + "/")).FirstOrDefault();

        public Type GetRouteType(string url)
        {
            var key = GetRoute(url);
            return key == null ? null : _routes[key];
        }

        public long? GetIdRoute(string url)
        {
            var id = url.Split("/").LastOrDefault();
            long value = 0;
            return long.TryParse(id, out value) ? value : default(long?);
        }

        public string ReplaceRoute(string currentRoute, string newRoute)
        {
            var key = GetRoute(currentRoute);
            return currentRoute.Replace(key, newRoute);
        }
    }
}
