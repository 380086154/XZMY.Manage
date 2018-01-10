using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using XZMY.Manage.Model.DataModel.Albums;
using XZMY.Manage.Model.ViewModel;
using XZMY.Manage.Model.ViewModel.Albums;
using XZMY.Manage.Service.Auth.Attributes;
using XZMY.Manage.Service.Handlers;
using T2M.Common.DataServiceComponents.Service;

namespace XZMY.Manage.Web.Controllers.Albums
{
    /// <summary>
    /// 
    /// </summary>
    public class AlbumsController : ControllerBase
    {
        //[AutoCreateAuthAction(Name = "图片列表", Code = "ImageUploadList", ModuleCode = "SYSTEM", Url = "/Albums/List", Visible = true, Remark = "")]
        public ActionResult List()
        {
            ViewData["imgpath"] = IMGPATH.Replace("\\", "/");
            return View();
        }
        public ActionResult Edit(Guid? Id)
        {
            VmAlbum model = new VmAlbum();
            if (Id.HasValue)
            {
                model = GetModel(Id.Value);
            }
            return View(model);
        }
        #region Ajax
        public ActionResult AjaxEdit(VmAlbum model)
        {
            Guid AlbumId = CreateEdit(model);
            if (AlbumId == Guid.Empty)
            {
                return Json(new { success = false, Id = AlbumId, errors = GetErrors() });
            }
            else
            {
                return Json(new { success = true, Id = AlbumId, errors = GetErrors() });
            }
        }
        #endregion
        // GET: Album
        //[AutoCreateAuthAction(Name = "上传图片", Code = "ImageUpload", ModuleCode = "SYSTEM", Url = "/Albums/Upload", Visible = true, Remark = "")]
        public ActionResult Upload()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var handler = new BaseDeleteService<Album>(id);
            var res = handler.Invoke();

            return Json(new { success = res, Id = id, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxList(VmSearchBase model)
        {
            var service = new GetEntityListService<Album>
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
            };
            var result = service.Invoke();
            return Json(new { success = true, total = result.TotalCount, rows = result.Results, errors = GetErrors() }, JsonRequestBehavior.AllowGet);
        }

        public static readonly string IMGPATH = WebConfigurationManager.AppSettings["UploadedImgPath"] ?? "Images/WallImages";
        public ActionResult Save(VmAlbum vm)
        {
            vm.DataId = Guid.NewGuid();
            vm.Detail = vm.Title = vm.Url;
            var handler = new BaseCreateHandler<Album>(vm);
            var id = handler.Invoke();
            if (id.Code != 0)
                return Json(new { Message = id.Message });


            return Json(new { Message = id.Output });
        }
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file == null) continue;
                    if (file.ContentLength == 0) continue;

                    var ticks = Guid.NewGuid().ToString();
                    var stream = new MemoryStream();
                    CopyStream(file.InputStream, stream);
                    var ext = CheckExt(stream);
                    ticks += "." + ext;
                    //Save file content goes here
                    fName = ticks;

                    var originalDirectory = string.Format("{0}{1}", Server.MapPath(@"\"), IMGPATH);

                    string pathString = originalDirectory;

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, ticks);
                    var filestream = new FileStream(path, FileMode.Create);
                    CopyStream(stream, filestream);
                    filestream.Flush();
                    filestream.Close();

                    var vm = new VmAlbum()
                    {
                        DataId = Guid.NewGuid(),
                        Title = file.FileName,
                        Url = ticks
                    };

                    var handler = new BaseCreateHandler<Album>(vm);
                    var id = handler.Invoke();
                    if (id.Code != 0)
                        return Json(new { Message = id.Message });

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        public static void CopyStream(Stream input, Stream output)
        {
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            while (true)
            {
                int read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    output.Flush();
                    output.Position = 0;
                    return;
                }
                output.Write(buffer, 0, read);
            }
        }
        /// <summary>
        /// 通过文件头信息判断文件类型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string CheckExt(Stream fs)
        {
            var r = new BinaryReader(fs);

            var bx = string.Empty;

            try
            {
                fs.Position = 0;
                byte buffer = r.ReadByte();
                bx = buffer.ToString();
                buffer = r.ReadByte();
                bx += buffer.ToString();

                // 208207 xls
                // 8075 xlsx
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                fs.Position = 0;
            }
            if (bx == "7173") return "gif";
            if (bx == "255216") return "jpg";
            if (bx == "13780") return "png";
            if (bx == "6677") return "bmp";
            return bx;
        }

        #region 功能
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public VmAlbum GetModel(Guid Id)
        {
            var service = new GetEntityByIdService<Album>(Id);
            var res = service.Invoke();

            var model = res.CreateViewModel<Album, VmAlbum>();
            if (model.Url.IndexOf(AlbumsController.IMGPATH) > -1)
            {
                model.Url = string.Format("{0}{1}", AlbumsController.IMGPATH, model.Url);
            }
            if (model.Thumbnail.IndexOf(AlbumsController.IMGPATH) > -1)
            {
                model.Thumbnail = string.Format("{0}{1}", AlbumsController.IMGPATH, model.Thumbnail);
            }
            return model;
        }
        /// <summary>
        /// 创建修改对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Guid CreateEdit(VmAlbum model)
        {
            Guid returnId = Guid.Empty;
            if (model.DataId == Guid.Empty)
            {
                var handler = new BaseCreateHandler<Album>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = res.Output;
                }
            }
            else
            {
                var handler = new BaseModifyHandler<Album>(model);
                var res = handler.Invoke();
                if (res.Success)
                {
                    returnId = model.DataId;
                }
            }
            return returnId;
        }
        #endregion
    }
}