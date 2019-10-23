using System;

namespace Infrastructure.CrossCutting.WebSockets
{
    public class WebSocketIdentifier
    {
        public string Id { get; set; }
        public string Channel { get; set; }

        public WebSocketIdentifier(string channel)
        {
            Id = Guid.NewGuid().ToString();
            Channel = channel;
        }

        public override string ToString()
        {
            return $"{Channel}{Id}";
        }
    }
}
