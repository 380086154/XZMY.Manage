using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Utility
{
    /// <summary>
    /// 获取硬件信息
    /// </summary>
    public class HardwareUtility
    {
        public string CpuID; //1.cpu序列号
        public string MacAddress; //2.mac序列号
        public string DiskID; //3.硬盘id
        public string IpAddress; //4.ip地址
        public string LoginUserName; //5.登录用户名
        public string ComputerName; //6.计算机名
        public string SystemType; //7.系统类型
        public string TotalPhysicalMemory; //8.内存量 单位：M

        public HardwareUtility()
        {
            CpuID = GetCpuID();
            MacAddress = GetMacAddress();
            DiskID = GetDiskID();
            IpAddress = GetIPAddress();
            LoginUserName = GetUserName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }

        //1.获取CPU序列号代码
        string GetCpuID()
        {
            try
            {
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        //2.获取网卡硬件地址
        string GetMacAddress()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        //3.获取硬盘ID 
        string GetDiskID()
        {
            try
            {
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        //4.获取IP地址
        string GetIPAddress()
        {
            try
            {
                //事先不知道ip的个数，数组长度未知，因此用StringCollection储存
                var localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                var ipCollection = new StringCollection();
                foreach (IPAddress ip in localIPs)
                {
                    //根据AddressFamily判断是否为ipv4,如果是InterNetWork则为ipv6
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        ipCollection.Add(ip.ToString());
                }
                var ipArray = new string[ipCollection.Count];
                ipCollection.CopyTo(ipArray, 0);

                return string.Join("", ipArray);//返回字IPv4符串
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        /// 5.操作系统的登录用户名 
        string GetUserName()
        {
            try
            {
                return Environment.UserName;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        //6.获取计算机名
        string GetComputerName()
        {
            try
            {
                //return Dns.GetHostName();
                return Environment.MachineName;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        ///7 PC类型 
        string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["SystemType"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }

        /// 8.物理内存
        public string GetTotalPhysicalMemory()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["TotalPhysicalMemory"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
    }
}
