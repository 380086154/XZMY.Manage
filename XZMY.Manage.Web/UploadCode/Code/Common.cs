using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace XZMY.Manage.Web.UploadCode.Code
{
    public class Common
    {
        #region
        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Rename(string fileName)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000) + Path.GetExtension(fileName);
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        public static string GetFileExt(string fileName)
        {
            return Path.GetExtension(fileName);
        }
        #endregion
    }
}