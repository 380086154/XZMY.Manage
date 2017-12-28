using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// 
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public static void Add(string context)
        {
#if DEBUG
            try
            {
                var path = PathUtility.databakPath + "log.txt";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                FileInfo finfo = new FileInfo(path);
                using (FileStream fs = finfo.OpenWrite())
                {
                    //根据上面创建的文件流创建写数据流
                    StreamWriter w = new StreamWriter(fs);
                    //设置写数据流的起始位置为文件流的末尾
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    //写入“Log   Entry   :   ”
                    w.Write("\r\n Log   Entry   ：   ");
                    //写入当前系统时间并换行
                    w.Write("{0}   {1}   \r\n ", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                    //写入日志内容并换行
                    w.Write(context + "\r\n ");
                    //写入------------------------------------“并换行
                    w.Write("------------------------------------\r\n ");
                    //清空缓冲区内容，并把缓冲区内容写入基础流
                    w.Flush();
                    //关闭写数据流
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
#endif
        }
    }
}
