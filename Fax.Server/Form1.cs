using Common;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Windows.Forms;
namespace Fax.Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpChannel channel = new HttpChannel(8090);
            ChannelServices.RegisterChannel(channel,false);

            RemotingConfiguration.RegisterWellKnownServiceType(
             typeof(FaxBussiness), "FaxBusiness.soap", WellKnownObjectMode.Singleton);
           // FaxBusiness.FaxSendedEvent += new FaxEventHandler(OnFaxSended);
            FaxBussiness.FaxSendedEvent += FaxBussiness_FaxSendedEvent;
        }

        private void FaxBussiness_FaxSendedEvent(string fax)
        {
            this.Invoke(new Action(()=> {
                this.listBox.Text += fax;
                this.listBox.Text += System.Environment.NewLine;
            }));

        }
    }
}
