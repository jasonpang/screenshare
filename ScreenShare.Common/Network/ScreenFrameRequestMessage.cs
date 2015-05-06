using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;

namespace ScreenShare.Common.Network
{
    public sealed class ScreenFrameRequestMessage : ScreenshareMessage
    {

        public ScreenFrameRequestMessage()
            : base(ScreenshareMessageType.ScreenFrameRequestMessage) { }

        public override void WritePayload(ISerializationContext context, IValueWriter writer) { }

        public override void ReadPayload(ISerializationContext context, IValueReader reader) { }
    }
}
