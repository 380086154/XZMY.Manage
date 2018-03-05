using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.IO;
using XZMY.Manage.WindowsService.Service;
using System.Threading;

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

            //var b2fs = new BackupToFileService();
            //var path = b2fs.CopyDataToBackup();
            //b2fs.WriteDataToServer(path);
            //return;

            //var erre = new RandomTimeService();
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(erre.Minute);
            //}
            //return;

            var dataPath = PathUtility.dataPath;
            var connectionStringService = new ConnectionStringService();
            var db = connectionStringService.InitDatabaseHelper(dataPath);
            var xfxxService = new XfxxService(db);//检查自定义字段是否存在

            //var totalCount = xfxxService.GetTotalCount();
            //var pageSize = 3;
            //var pageIndex = (int)Math.Ceiling((double)(totalCount / pageSize));
            //pageIndex = pageIndex == 0 ? 1 : pageIndex;

            //var sql = "SELECT TOP " + pageSize + " * FROM [xfxx] WHERE fdid not in (SELECT TOP " + (pageIndex * pageSize) + " fdid FROM [xfxx] )";
            //var dt = db.GetDataTable(sql, "xfxx", EProviderName.OleDB);



            //return;


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BackupToFileService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
