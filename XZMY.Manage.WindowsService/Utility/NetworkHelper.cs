using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Utility
{
    /// <summary>
    /// 网络
    /// </summary>
    public class NetworkHelper
    {
        /// <summary>
        /// 网络帮助类
        /// </summary>
        public NetworkHelper() { }

        /// <summary>
        /// 判断网络连接
        /// </summary>
        /// <param name="connectionDescription"></param>
        /// <param name="reservedValue"></param>
        /// <returns></returns>
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        /// <summary>
        /// 获取当前计算机联网状态
        /// </summary>
        public bool Status
        {
            get
            {
                int i = 0;
                var result = InternetGetConnectedState(out i, 0);//硬件已接入网络

                if (result)
                {
                    result = PingNetAddress();//判断是否接入互联网
                }
                return result;
            }
        }

        #region Private method

        /// <summary>
        /// 通过 ping www.baidu.com 判断是否已连接到互联网
        /// </summary>
        /// <returns></returns>
        private bool PingNetAddress()
        {
            var flag = false;
            var ping = new Ping();
            try
            {
                var pr = ping.Send("www.baidu.com", 3000);

                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }

                if (pr.Status == IPStatus.TimedOut)
                {
                    flag = false;
                }
            }
            catch (PingException ex)
            {
                flag = false;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        #endregion
    }
}
