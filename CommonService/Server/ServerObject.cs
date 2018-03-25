using RemotingDemo.CommonService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace RemotingDemo.CommonService.Server
{
    public interface IServeObject
    {
        Person GetPersonInfo(string name, string sex, int age);
    }
    public class ServerObject:MarshalByRefObject,IServeObject
    {
        public ServerObject()
        {

        }

        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(3);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(40);
            }
            return lease;
        }
        public Person GetPersonInfo(string name,string sex,int age)
        {
            Person person = new Person
            {
                Name = name,
                Sex = sex,
                Age = age
            };
            return person;
        }
    }
}
