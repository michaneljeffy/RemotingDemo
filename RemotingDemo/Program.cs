using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using RemotingDemo.CommonService.Server;
using System.Collections;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Services;
using CommonService.Server;

namespace RemotingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StartChannel();

            Console.Read();
        }

        private static void StartChannel()
        {
            TcpChannel tcpChannel = new TcpChannel(8080);
            ChannelServices.RegisterChannel(tcpChannel, true);

            //添加服务跟踪
            TrackingServices.RegisterTrackingHandler(new MyTrackingHandler());
            //注册远程对象
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(ServerObject), "ServiceMessage", WellKnownObjectMode.SingleCall);

            ObjRef objRef = RemotingServices.Marshal(new ServerFactory(),"FactoryService");
            //RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerObject),
            //    "ServiceMessage",WellKnownObjectMode.Singleton);

            //RemotingConfiguration.ApplicationName = "ServiceMessage";
            //RemotingConfiguration.RegisterActivatedServiceType(typeof(ServerObject));

            Console.WriteLine("Remoting服务启动，按退出...");
            Console.ReadLine();


        }

        private static void CloseChannel()
        {
            IChannel[] channels = ChannelServices.RegisteredChannels;

            foreach(var item in channels)
            {
                if(item.ChannelName == "ServiceMessage")
                {
                    TcpChannel tcpChannel = (TcpChannel)item;
                    tcpChannel.StopListening(null);
                    ChannelServices.UnregisterChannel(tcpChannel);
                }
            }
        }


        private static void StartMultipHttp()
        {
            IDictionary httpProp = new Hashtable();
            httpProp["name"] = "http8080";
            httpProp["port"] = 8080;
            IChannel channel = new HttpChannel(httpProp,
             new SoapClientFormatterSinkProvider(),
             new SoapServerFormatterSinkProvider());
            ChannelServices.RegisterChannel(channel,true);
        }

        private static void StartMultipTCP()
        {
            IDictionary tcpProp = new Hashtable();
            tcpProp["name"] = "tcp9090";
            tcpProp["port"] = 9090;
            IChannel channel = new TcpChannel(tcpProp,
             new BinaryClientFormatterSinkProvider(),
             new BinaryServerFormatterSinkProvider());
            ChannelServices.RegisterChannel(channel,true);
        }
    }
}
