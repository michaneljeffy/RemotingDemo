using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using RemotingDemo.CommonService.Model;
using RemotingDemo.CommonService.Server;

namespace RemotingDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel,true);

            ServerObject serverObj = (ServerObject)Activator.GetObject(
                typeof(ServerObject),"tcp://localhost:8080/ServiceMessage");

            Person person = serverObj.GetPersonInfo("张舒禹","男",25);

            Console.WriteLine($"{person.Name }{person.Sex}{person.Age}");
        }

        public static void Sponsor(ServerObject obj)
        {
            ClientSponsor sponsor = new ClientSponsor();
            sponsor.RenewalTime = TimeSpan.FromMinutes(2);
            sponsor.Register(obj);
        }
    }
}
