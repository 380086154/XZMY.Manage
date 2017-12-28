using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace XZMY.Manage.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //string path = PathUtility.path;
            //FileUtility fileUtility = new FileUtility();
            //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            //System.IO.FileInfo[] fileList = di.GetFiles();
            //FileComparer fc = new FileComparer();
            //Array.Sort(fileList, fc);//按文件创建时间排正序

            //var len = fileList.Length - 166;

            ////删除超出预留文件数以外的文件
            //for (int i = len; i > 0; --i)
            //{
            //    var file = fileList[i];
            //    if (file.FullName.Contains(".xml")) continue;
            //    fileUtility.DeleteFile(file.FullName);//删除文件
            //}


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BackupToFileService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
