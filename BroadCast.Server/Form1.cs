using Common;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
namespace BroadCast.Server
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
            StartServer();
        }

        private void FaxBussiness_FaxSendedEvent(string fax)
        {
            this.Invoke(new Action(()=> {
                this.listBox.Text += fax;
                this.listBox.Text += System.Environment.NewLine;
            }));

        }

        private Common.BroadCast Obj;
        private void StartServer()
        {
            BinaryServerFormatterSinkProvider serverProvider = new
             BinaryServerFormatterSinkProvider();
            BinaryClientFormatterSinkProvider clientProvider = new
             BinaryClientFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["port"] = 8080;
            HttpChannel channel = new HttpChannel(props, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel,false);

            Obj = new Common.BroadCast();
            ObjRef objRef = RemotingServices.Marshal(Obj, "BroadCastMessage.soap");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox.Text != string.Empty)
            {
                this.Obj.BroadCastingInfo(listBox.Text);
            }
            else
            {
                MessageBox.Show("请输入信息！");
            }
        }
    }
}
