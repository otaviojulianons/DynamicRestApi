using Infrastructure.Services.WebSockets;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection UseWebSocketService(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketService>();
            return services;
        }
    }
}
