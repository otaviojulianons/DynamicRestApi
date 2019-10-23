using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.WebSockets
{
    public class WebSocketService
    {
        public ConcurrentDictionary<WebSocketIdentifier, WebSocket> WebSockets { get; private set; }
        public Dictionary<string,Type> Channels { get; private set; }

        public WebSocketService()
        {
            WebSockets = new ConcurrentDictionary<WebSocketIdentifier, WebSocket> ();
            Channels = new Dictionary<string, Type>();
        }

        public WebSocketHandler AddWebSocket(string channel, WebSocket webSocket)
        {
            var identifier = new WebSocketIdentifier(channel);
            var webSocketHandler = new WebSocketHandler(identifier, webSocket);
            WebSockets.TryAdd(identifier, webSocket);
            return webSocketHandler;
        }

        public List<WebSocket> GetAll(string channel)
        {
            return WebSockets.Where( x => x.Key.Channel == channel).Select(x => x.Value).ToList();
        }

        public async Task Remove(WebSocketIdentifier id)
        {
            WebSocket socket;
            WebSockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }




    }
}
