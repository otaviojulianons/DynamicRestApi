using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DynamicRoutesCollection
    {
        
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();  

        public void AddRoute(string route,Type type)
        {
            if (!route.StartsWith("/"))
                route = $"/{route}";

            Routes.TryAdd(route, type);
        }
    }
}
