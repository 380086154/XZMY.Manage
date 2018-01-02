using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XZMY.Manage.Web.UploadCode.Code
{
    public class ResultData
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        public class UploadImageInfo
        {
            /// <summary>
            /// 状态
            /// </summary>
            public int status
            {
                get { return _status; }
                set { _status = value; }
            }
            private int _status = 0;
            /// <summary>
            /// 图片地址
            /// </summary>
            public string path
            {
                get { return _path; }
                set { _path = value; }
            }
            private string _path = string.Empty;

            /// <summary>
            /// 缩略图地址
            /// </summary>
            public string thumb
            {
                get { return _thumb; }
                set { _thumb = value; }
            }
            private string _thumb = string.Empty;

            /// <summary>
            /// 文件名
            /// </summary>
            public string name
            {
                get { return _filename; }
                set { _filename = value; }
            }
            private string _filename = string.Empty;

            /// <summary>
            /// 文件大小
            /// </summary>	
            public int size
            {
                get { return _size; }
                set { _size = value; }
            }
            private int _size = 0;

            /// <summary>
            /// 消息
            /// </summary>
            public string msg
            {
                get { return _msg; }
                set { _msg = value; }
            }
            private string _msg = string.Empty;

            /// <summary>
            /// 扩展名
            /// </summary>
            public string extension
            {
                get { return _extension; }
                set { _extension = value; }
            }
            private string _extension = string.Empty;
        }

    }
}