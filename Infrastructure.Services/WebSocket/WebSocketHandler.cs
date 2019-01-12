using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.WebSockets
{
    public class WebSocketHandler
    {
        private WebSocketService _service;
        private WebSocket _webSocket;
        private WebSocketKey _id;

        public WebSocketHandler(WebSocketService service, WebSocket webSocket,string channel)
        {
            _service = service;
            _webSocket = webSocket;
            _id = new WebSocketKey(channel);
        }

        public async Task Invoke(HttpContext context,object initialData)
        {
            await _service.AddWebSocketHandler(this);
            await _webSocket.SendData(initialData);

            var data = await _webSocket.ReceiveData();
            while (!data.result.CloseStatus.HasValue)
            {
                await _service.SendAll(_id.Channel,data.GetValue());
                data = await _webSocket.ReceiveData();
            }
            await _webSocket.CloseAsync(data.result.CloseStatus.Value, data.result.CloseStatusDescription, CancellationToken.None);
        }

        public WebSocket GetWebSocket() => _webSocket;

        public WebSocketKey GetId() => _id;


    }
}
