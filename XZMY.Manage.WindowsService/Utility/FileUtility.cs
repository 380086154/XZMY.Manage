using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// 文件及文件操作类
    /// </summary>
    public class FileUtility
    {
        public FileUtility()
        {

        }

        /// <summary>
        /// C# 删除文件夹，以及文件夹下的文件
        /// 用法： DeleteFolder(@"c:\\1");
        /// </summary>
        /// <param name="path">待删除文件夹完整路径</param>
        public void DeleteFolder(string path)
        {
            // 循环文件夹里面的内容
            foreach (var folder in Directory.GetFileSystemEntries(path))
            {
                try
                {
                    // 如果文件存在
                    if (File.Exists(folder))
                    {
                        var file = new FileInfo(folder) { Attributes = FileAttributes.Normal };

                        //if (fi.Attributes.ToString().IndexOf("Readonly") != 1)
                        //{//感觉没必要判断，反正都要操作那就所有都是正常

                        //}

                        File.Delete(folder);// 直接删除其中的文件
                    }
                    else
                    {
                        // 如果文件夹存在子文件夹
                        // 递归删除子文件夹
                        DeleteFolder(folder);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            try
            {
                Directory.Delete(path);// 删除已空文件夹
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">待删除文件夹完整路径</param>
        public void DeleteFile(string path)
        {
            try
            {
                var file = new FileInfo(path) { Attributes = FileAttributes.Normal };
                File.Delete(path);// 直接删除其中的文件

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 删除遗留下来的文件夹
        /// </summary>
        /// <param name="path">待删除文件夹完整路径</param>
        public void RevmoeEmptyFolder(string path)
        {
            foreach (var folder in Directory.GetDirectories(path))
            {
                DeleteFolder(folder);
            }
        }

        private string ExistsWinRar()
        {
            var winRarPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/winrar.exe";

            if (File.Exists(winRarPath))
                return winRarPath;

            return "";
        }

        /// <summary>
        /// 生成Zip压缩包
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="rarPath">rar 存放位置</param>
        /// <param name="rarName">生成压缩文件的文件名</param>
        public void CompressRar(string path, string rarPath, string rarName)
        {
            try
            {
                using (var process = new Process())
                {
                    var winRarPath = ExistsWinRar();

                    if (string.IsNullOrWhiteSpace(winRarPath)) return; //验证WinRar是否安装。

                    var pathInfo = String.Format("a -afzip -m3 -ep1 \"{0}\" \"{1}\"", rarName, path);

                    #region WinRar 用到的命令注释

                    //[a] 添加到压缩文件
                    //afzip 执行zip压缩方式，方便用户在不同环境下使用。（取消该参数则执行rar压缩）
                    //-m0 存储 添加到压缩文件时不压缩文件。共6个级别【0-5】，值越大效果越好，也越慢
                    //ep1 依名称排除主目录（生成的压缩文件不会出现不必要的层级）
                    //r   修复压缩档案
                    //t   测试压缩档案内的文件 
                    //as  同步压缩档案内容  
                    //-p  给压缩文件加密码方式为：-p123456

                    #endregion

                    //打包文件存放目录
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = winRarPath, //执行的文件名
                        Arguments = pathInfo, //需要执行的命令
                        UseShellExecute = false, //使用Shell执行
                        WindowStyle = ProcessWindowStyle.Hidden, //隐藏窗体
                        WorkingDirectory = rarPath, //rar 存放位置
                        CreateNoWindow = true, //是隐藏窗体
                    };

                    process.Start(); //开始执行
                    process.WaitForExit(); //等待完成并退出
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 文件监听



        #endregion
    }
}
