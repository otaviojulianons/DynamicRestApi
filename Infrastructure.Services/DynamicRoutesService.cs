using Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services
{
    public class DynamicRoutesService : IDynamicRoutesService
    {
        
        private Dictionary<string, Type> _routes = new Dictionary<string, Type>();  

        private string GetRouteKey(string url)
            => _routes.Keys.Where(key => key == url || url.StartsWith(key + "/")).FirstOrDefault();
        
        public void AddRoute(string route,Type type)
        {
            if (!route.StartsWith("/"))
                route = $"/{route}";

            _routes.TryAdd(route, type);
        }

        public bool IsMatch(string url)
        {
            return _routes.Keys.Where(key => key == url || url.StartsWith(key + "/")).Count() > 0;
        }

        public Type GetRouteType(string url)
        {
            var key = GetRouteKey(url);
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
            var key = GetRouteKey(currentRoute);
            return currentRoute.Replace(key, newRoute);
        }
    }
}
