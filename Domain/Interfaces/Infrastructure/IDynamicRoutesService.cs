using System;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicRoutesService
    {
        void AddRoute(string route, Type type);
        Type Get(string url);
        long? GetIdRoute(string url);
        bool IsMatch(string url);
    }
}