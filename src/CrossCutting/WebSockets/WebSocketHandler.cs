using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.WebSockets
{
    public class WebSocketHandler
    {
        private WebSocketService _service;
        private WebSocket _webSocket;
        private WebSocketIdentifier _identifier;

        public WebSocketHandler(WebSocketIdentifier identifier, WebSocket webSocket)
        {
            _webSocket = webSocket;
            _identifier = identifier;
        }

        public void SetManager(WebSocketService service) => _service = service;

        public async Task Invoke(HttpContext context,object initialData)
        {
            await _webSocket.SendData(initialData);

            var data = await _webSocket.ReceiveData();
            while (!data.result.CloseStatus.HasValue)
            {
                await _service?.SendAll(_identifier.Channel,data.GetValue());
                data = await _webSocket.ReceiveData();
            }
            await _webSocket.CloseAsync(data.result.CloseStatus.Value, data.result.CloseStatusDescription, CancellationToken.None);
        }

        public WebSocket GetWebSocket() => _webSocket;

        public WebSocketIdentifier GetId() => _identifier;


    }
}
