using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace ScreenShare.Common.Network
{
    public enum ScreenshareMessageType : ushort
    {
        ScreenFrameRequestMessage = 1,
        ScreenFrameResponseMessage = 2
    }

    public abstract class ScreenshareMessage : Message
    {
        protected ScreenshareMessage(ScreenshareMessageType type)
            : base(ScreenshareProtocol.Instance, (ushort)type)
        {
        }
    }

    public static class ScreenshareProtocol
    {
        public static Protocol Instance = new Protocol(2);

        static ScreenshareProtocol()
        {
            // We need to tell our protocol about all the message
            // types belonging to it. Discover() does this automatically.
            Instance.Discover();
        }
    }
}
