using ScreenShare.Client.Network;
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
using Tempest;
using ScreenShare.Client.Extensions;

namespace ScreenShare.Client
{
    public partial class FormMain : Form
    {
        private ScreenshareClient Client;

        public FormMain()
        {
            InitializeComponent();
        }

        private async void FormMain_Shown(object sender, EventArgs e)
        {
            Client = ScreenshareClient.CreateNew();
            Client.Connected += Client_Connected;
            Client.ScreenFrameReceived += Client_ScreenFrameReceived;
            await Client.ConnectAsync(new Target(Target.LoopbackIP, 58291));
        }

        private async void Client_ScreenFrameReceived(Bitmap screen)
        {
            this.SynchronizedInvoke(() => BackgroundImage = screen);
            await Client.SendScreenFrameRequestAsync();
        }

        private async void Client_Connected(object sender, ClientConnectionEventArgs e)
        {
            Debug.WriteLine(String.Format("Client: Connected to {0}:{1}!", e.Connection.RemoteTarget.Hostname, e.Connection.RemoteTarget.Port));
            await Client.SendScreenFrameRequestAsync();
        }
    }
}
