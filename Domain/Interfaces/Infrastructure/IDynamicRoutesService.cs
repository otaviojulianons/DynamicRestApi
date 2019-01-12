using System;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicRoutesService
    {
        void AddRoute(string route, Type type);
        Type GetRouteType(string url);
        long? GetIdRoute(string url);
        string GetRoute(string url);
        bool IsMatch(string url);
        string FindRoute(string url);
        string ReplaceRoute(string currentRoute, string newRoute);
    }
}