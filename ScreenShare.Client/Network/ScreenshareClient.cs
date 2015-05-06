using ScreenShare.Common.Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;
using Tempest.Providers.Network;

namespace ScreenShare.Client.Network
{
    public class ScreenshareClient : TempestClient
    {
        public ScreenshareClient(IClientConnection connection)
            : base(connection, MessageTypes.Reliable)
        {
            this.RegisterMessageHandler<ScreenFrameResponseMessage>(OnScreenFrameMessage);
        }

        public event Action<Bitmap> ScreenFrameReceived;

        public Task SendScreenFrameRequestAsync()
        {
            return Connection.SendAsync(new ScreenFrameRequestMessage());
        }

        private void OnScreenFrameMessage(MessageEventArgs<ScreenFrameResponseMessage> e)
        {
            if (ScreenFrameReceived != null)
                ScreenFrameReceived(e.Message.ScreenFrame);
        }

        public static ScreenshareClient CreateNew()
        {
            var connection = new NetworkClientConnection(ScreenshareProtocol.Instance);
            return new ScreenshareClient(connection);
        }
    }
}
