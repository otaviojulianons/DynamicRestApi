using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.WebSockets
{
    public class WebSocketService
    {
        public ConcurrentDictionary<WebSocketKey, WebSocket> WebSockets { get; private set; }
        public Dictionary<string,Type> Channels { get; private set; }

        public WebSocketService()
        {
            WebSockets = new ConcurrentDictionary<WebSocketKey, WebSocket> ();
            Channels = new Dictionary<string, Type>();
        }

        public async Task AddWebSocketHandler(WebSocketHandler socket)
        {
            WebSockets.TryAdd(socket.GetId(),socket.GetWebSocket());
        }

        public List<WebSocket> GetAll(string channel)
        {
            return WebSockets.Where( x => x.Key.Channel == channel).Select(x => x.Value).ToList();
        }

        public async Task Remove(WebSocketKey id)
        {
            WebSocket socket;
            WebSockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }




    }
}
