using RemotingDemo.CommonService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Server
{
    public interface IServerFactory
    {
        IServeObject CreateInstance();
    }
    public class ServerFactory:MarshalByRefObject,IServerFactory
    {
        public IServeObject CreateInstance()
        {
            return new ServerObject();
        }

        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if(lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(1);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(20);
            }

            return lease;
        }
    }
}
