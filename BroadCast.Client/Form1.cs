using Common;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
namespace BroadCast.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = true;
            InitializeComponent();
        }

        private static IBroadCast watch;
        private static EventWrapper wrapper;
        private void Form1_Load(object sender, EventArgs e)
        {
            BinaryServerFormatterSinkProvider serverProvider = new
            BinaryServerFormatterSinkProvider();
            BinaryClientFormatterSinkProvider clientProvider = new
            BinaryClientFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["port"] = 0;
            HttpChannel channel = new HttpChannel(props, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel,false);

            watch = (IBroadCast)Activator.GetObject(
             typeof(IBroadCast), "http://localhost:8080/BroadCastMessage.soap");

            wrapper = new EventWrapper();
            wrapper.LocalBroadCastEvent += new BroadCastEventHandler(BroadCastingMessage);
            watch.BroadCastEvent += new BroadCastEventHandler(wrapper.BroadCasting);

            //watch.BroadCastEvent += new BroadCastEventHandler(BroadCastingMessage);
        }

        private void BroadCastingMessage(string fax)
        {
            this.Invoke(new Action(()=> {
                this.listBox.Text += fax;
                this.listBox.Text += System.Environment.NewLine;
            }));

        }
 
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            watch.BroadCastEvent -= new BroadCastEventHandler(wrapper.BroadCasting);
        }
    }
}
