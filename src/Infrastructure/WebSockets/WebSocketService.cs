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
        private Dictionary<string, Type> _channels;

        public WebSocketService()
        {
            WebSockets = new ConcurrentDictionary<WebSocketIdentifier, WebSocket> ();
            _channels = new Dictionary<string, Type>();
        }

        public void AddChannel(string name, Type type) =>
            _channels.Add(FormatChannelName(name), type);

        public void RemoveChannel(string name) =>
            _channels.Remove(FormatChannelName(name));

        private string FormatChannelName(string name) => $"/{name}/Subscribe";

        public Type GetChannelType(string channelName) => 
            _channels.TryGetValue(channelName, out Type type) ? type : null;

        public List<WebSocket> GetWebSockets(string channel)
        {
            return WebSockets.Where(x => x.Key.Channel == channel).Select(x => x.Value).ToList();
        }

        public WebSocketHandler AddWebSocket(string channel, WebSocket webSocket)
        {
            var identifier = new WebSocketIdentifier(channel);
            var webSocketHandler = new WebSocketHandler(identifier, webSocket);
            WebSockets.TryAdd(identifier, webSocket);
            return webSocketHandler;
        }

        public async Task RemoveWebSocket(WebSocketIdentifier id)
        {
            WebSocket socket;
            WebSockets.TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }
    }
}
