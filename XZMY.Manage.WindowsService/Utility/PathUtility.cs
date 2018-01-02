using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// 自动获取正确盘符
    /// </summary>
    public class PathUtility
    {
        /// <summary>
        /// 备份目录
        /// </summary>
        public static string databakPath = @"{0}:\Program Files (x86)\美萍会员管理系统\databak\";
        /// <summary>
        /// 数据文件
        /// </summary>
        public static string dataPath = @"{0}:\Program Files (x86)\美萍会员管理系统\data\";

        public PathUtility()
        {
            dataPath = GetPath(dataPath);
            databakPath =GetPath(databakPath);            
        }

        static PathUtility()
        {
            dataPath = GetPath(dataPath);
            databakPath = GetPath(databakPath);
        }

        public static string GetPath(string patch)
        {
            //ASCII 65-90 (大写字母 A-Z)
            for (int i = 65; i < 91; i++)
            {
                var p = string.Format(patch, (char)i);
                if (Directory.Exists(p))
                {
                   return p;
                }
            }
            return string.Format(patch, "C");
        }
    }
}
