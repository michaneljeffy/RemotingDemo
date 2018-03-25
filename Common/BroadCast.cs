using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public delegate void BroadCastEventHandler(string info);

    public interface IBroadCast
    {
        event BroadCastEventHandler BroadCastEvent;
        void BroadCastingInfo(string info);
    }


    public class BroadCast:MarshalByRefObject,IBroadCast
    {
        public event BroadCastEventHandler BroadCastEvent;

        #region IBroadCast 成员

        //[OneWay]
        public void BroadCastingInfo(string info)
        {
            if (BroadCastEvent != null)
            {
                BroadCastEventHandler tempEvent = null;

                int index = 1; //记录事件订阅者委托的索引，为方便标识，从1开始。
                foreach (Delegate del in BroadCastEvent.GetInvocationList())
                {
                    try
                    {
                        tempEvent = (BroadCastEventHandler)del;
                        tempEvent(info);
                    }
                    catch
                    {
                        MessageBox.Show("事件订阅者" + index.ToString() + "发生错误,系统将取消事件订阅!");
                        BroadCastEvent -= tempEvent;
                    }
                    index++;
                }
            }
            else
            {
                MessageBox.Show("事件未被订阅或订阅发生错误!");
            }
        }

        #endregion

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
