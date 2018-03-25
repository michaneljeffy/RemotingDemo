using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public delegate void FaxEventHandler(string fax);
    public interface IFaxBussiness
    {
        void SendFax(string fax);
    }
}
