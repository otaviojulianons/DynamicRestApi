using Infrastructure.Repository.Repositories;
using Infrastructure.Services.WebSockets;
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
                var repositoryType = typeof(DynamicRepository<>).MakeGenericType(type);
                dynamic repository = context.RequestServices.GetService(repositoryType);
                var initialData = repository.GetAll();
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await new WebSocketHandler(_webSocketService, webSocket, context.Request.Path).Invoke(context, initialData);
                }
                else
                    context.Response.StatusCode = 200;
            }
            else
                _next(context);
        }

    }
}


