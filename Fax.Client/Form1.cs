using Common;
using System;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Windows.Forms;

namespace Fax.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = true;
            InitializeComponent();
        }
        private static IFaxBussiness faxBus;
        private void Form1_Load(object sender, EventArgs e)
        {
            HttpChannel channel = new HttpChannel(0);
            ChannelServices.RegisterChannel(channel,false);

            faxBus = (IFaxBussiness)Activator.GetObject(typeof(IFaxBussiness),
             "http://localhost:8090/FaxBusiness.soap");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox.Text != String.Empty)
            {
                string fax = "来自" + GetIpAddress() + "客户端的传真:"
              + System.Environment.NewLine;
                fax += listBox.Text;
                this.Invoke(new Action(()=> {
                    faxBus.SendFax(fax);
                }));
            }
            else
            {
                MessageBox.Show("请输入传真内容!");
            }
        }

        private string GetIpAddress()
        {
            IPHostEntry ipHE = Dns.GetHostEntry(Dns.GetHostName());
            return ipHE.AddressList[0].ToString();
        }
    }
}
