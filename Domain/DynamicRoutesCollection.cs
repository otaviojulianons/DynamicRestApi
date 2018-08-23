using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain
{
    public class DynamicRoutesCollection
    {
        
        private Dictionary<string, Type> _routes = new Dictionary<string, Type>();  

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

        public Type Get(string url)
        {
            var find = _routes.Keys.Where(key => key == url || url.StartsWith(key + "/")).FirstOrDefault();
            return find == null ? null : _routes[find];
        }

        public long? GetIdRoute(string url)
        {
            var id = url.Split("/").LastOrDefault();
            long value = 0;
            return long.TryParse(id, out value) ? value : default(long?);
        }


    }
}
