using ScreenShare.Common.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;
using Tempest.Providers.Network;

namespace ScreenShare.Server.Network
{
    public sealed class ScreenshareServer : TempestServer
    {
        public ScreenshareServer(IConnectionProvider provider)
            : base(provider, MessageTypes.Reliable)
        {
            this.RegisterMessageHandler<ScreenFrameRequestMessage>(OnScreenFrameRequestMessage);
        }

        public event Action ScreenFrameRequestReceived;

        private readonly List<IConnection> connections = new List<IConnection>();
        private void OnScreenFrameRequestMessage(MessageEventArgs<ScreenFrameRequestMessage> e)
        {
            Debug.WriteLine(String.Format("Server: OnScreenFrameRequestMessage() {0}", DateTime.Now.ToLongTimeString()));
            if (ScreenFrameRequestReceived != null)
                ScreenFrameRequestReceived();
        }

        protected override void OnConnectionMade(object sender, ConnectionMadeEventArgs e)
        {
            lock (this.connections)
                this.connections.Add(e.Connection);

            base.OnConnectionMade(sender, e);
        }

        protected override void OnConnectionDisconnected(object sender, DisconnectedEventArgs e)
        {
            lock (this.connections)
                this.connections.Remove(e.Connection);

            base.OnConnectionDisconnected(sender, e);
        }

        public void SendScreenFrameResponse(Bitmap screenCapture)
        {
            lock (this.connections)
                foreach (IConnection connection in this.connections)
                    connection.SendAsync(new ScreenFrameResponseMessage() { ScreenFrame = screenCapture } );
        }

        public static ScreenshareServer StartNew()
        {
            // NetworkConnectionProvider requires that you tell it what local target to listen
            // to and the maximum number of connections you'll allow.
            var provider = new NetworkConnectionProvider(ScreenshareProtocol.Instance, new Target(Target.AnyIP, 58291), 10);

            var server = new ScreenshareServer(provider);
            server.Start();
            return server;
        }
    }
}
