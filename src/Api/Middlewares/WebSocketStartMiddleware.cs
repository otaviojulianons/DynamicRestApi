using Domain.Interfaces.Infrastructure;
using Infrastructure.CrossCutting.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class WebSocketStartMiddleware
    {
        private RequestDelegate _next;
        private WebSocketService _webSocketService;

        public WebSocketStartMiddleware(RequestDelegate next, WebSocketService webSocketService)
        {
            _next = next;
            _webSocketService = webSocketService;
        }

        public async Task Invoke(HttpContext context)
        {             
            if (_webSocketService.Channels.ContainsKey(context.Request.Path))
            {
                var type = _webSocketService.Channels[context.Request.Path];
                var repositoryType = typeof(IDynamicRepository<>).MakeGenericType(type);
                dynamic repository = context.RequestServices.GetService(repositoryType);
                var initialData = repository.GetAll();
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    string channel = context.Request.Path;
                    WebSocketHandler handler = _webSocketService.AddWebSocket(channel, webSocket);

                    await handler.Invoke(context, initialData);
                }
                else
                    context.Response.StatusCode = 200;
            }
            else
                await _next(context);
        }

    }
}


