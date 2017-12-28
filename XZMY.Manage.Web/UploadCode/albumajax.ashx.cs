using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using XZMY.Manage.Web.Controllers.Albums;

namespace XZMY.Manage.Web.UploadCode
{
    /// <summary>
    /// Summary description for ajax
    /// </summary>
    public class albumajax : IHttpHandler
    {

        string result = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string type = context.Request.QueryString["action"];

            switch (type)
            {
                case "helloworld":
                    HelloWord();
                    break;
                case "UpLoadFile":
                    UploadImage(context);
                    break;
                default:
                    //Uploader(context);
                    break;
            }
            context.Response.Write(result);
            context.Response.End();
        }

        private void UploadImage(HttpContext context)
        {
            string isthumb = context.Request.Form["thumbnail"];
            bool thumb = isthumb == "true" ? true : false;
            //上传图片           
            HttpPostedFile postedFile = context.Request.Files[0];
            var path = AlbumsController.IMGPATH;  //上传保存的路径
            int size = 2;   //文件大小限制,单位mb 

            Code.ImageHelper up = new Code.ImageHelper();

            //var upImage = up.UploadImage(postedFile, path, size, false);			
            //生成缩略图
            var upImage = up.UploadImage(postedFile, path, size, thumb, 200);
            //生成水印
            //var upImage = up.UploadImage(postedFile, path, size, WatermarkType.Text, "水印文字", WatermarkPosition.Center, false);
            //生成水印+缩略图
            //var upImage = up.UploadImage(postedFile, path, size, WatermarkType.Text, "水印文字", WatermarkPosition.Center, true, 200);

            result = JsonConvert.SerializeObject(upImage);

        }


        private void HelloWord()
        {
            result = "hello world";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}