using ScreenShare.Common.ScreenCapture;
using ScreenShare.Server.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShare.Server
{
    public partial class FormMain : Form
    {
        private ScreenshareServer Server;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Server = ScreenshareServer.StartNew();
            Server.ConnectionMade += Server_ConnectionMade;
            Server.ScreenFrameRequestReceived += Server_ScreenFrameRequestReceived;
        }

        void Server_ScreenFrameRequestReceived()
        {
            try
            {
                Bitmap screen = DxgiOutputDuplicator.CaptureScreen();
                Server.SendScreenFrameResponse(screen);
            }
            catch (Exception ex)
            {
            }
        }

        void Server_ConnectionMade(object sender, Tempest.ConnectionMadeEventArgs e)
        {
            Debug.WriteLine(String.Format("Server: Connection received from {0}:{1}!", e.Connection.RemoteTarget.Hostname, e.Connection.RemoteTarget.Port));
        }
    }
}
