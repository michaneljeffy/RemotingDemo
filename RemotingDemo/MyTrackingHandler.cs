using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace RemotingDemo
{
    public class MyTrackingHandler : ITrackingHandler
    {
        public MyTrackingHandler()
        {

        }

        public void DisconnectedObject(object obj)
        {
            Console.WriteLine();
            Console.WriteLine("对象" + obj.ToString() + " is disconnected at " + DateTime.Now.ToShortTimeString());
        }

        public void MarshaledObject(object obj, ObjRef or)
        {
            Console.WriteLine();
            Console.WriteLine("对象" + obj.ToString() + " is marshaled at " + DateTime.Now.ToShortTimeString());
        }

        public void UnmarshaledObject(object obj, ObjRef or)
        {
            Console.WriteLine();
            Console.WriteLine("对象" + obj.ToString() + " is unmarshaled at " + DateTime.Now.ToShortTimeString());
        }
    }
}
