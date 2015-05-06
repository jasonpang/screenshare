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
    public sealed class ScreenFrameResponseMessage : ScreenshareMessage
    {
        public Bitmap ScreenFrame { get; set; }

        public ScreenFrameResponseMessage()
            : base(ScreenshareMessageType.ScreenFrameResponseMessage)
        {
        }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            using (var ms = new MemoryStream())
            {
                ScreenFrame.Save(ms, ImageFormat.Png);
                writer.WriteBytes(ms.GetBuffer());
            }
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            var ms = new MemoryStream(reader.ReadBytes());
            ScreenFrame = new Bitmap(ms);
        }
    }
}
