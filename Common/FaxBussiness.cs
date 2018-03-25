using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FaxBussiness:MarshalByRefObject, IFaxBussiness
    {
        public static event FaxEventHandler FaxSendedEvent;
        public void SendFax(string fax)
        {
            if(FaxSendedEvent !=null)
            {
                FaxSendedEvent(fax);
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
