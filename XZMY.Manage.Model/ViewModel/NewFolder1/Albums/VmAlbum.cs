
using System;
using XZMY.Manage.Model.DataModel.Albums;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.ViewModel.Albums
{
    [Serializable]
    public class VmAlbum : IActionViewModel<Album>
    {
        #region Properties 

        /// <summary>
        /// 主键
        /// </summary>
        //[EntAttributes.DBColumn("Id")] 
        //[DisplayName("主键")] 
        public Guid DataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("")] 
        public String Title { get; set; }
        public String Detail { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Url")] 
        //[DisplayName("")] 
        public String Url { get; set; }
        public String Thumbnail { get; set; }

        #endregion

        #region Extendsions

        public Album CreateNewDataModel()
        {
            var model = new Album();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            //model.Id = Id;
            model.Title = Title;
            model.Url = Url;
            model.Detail = Detail;
            model.Thumbnail = Thumbnail;
            return model;
        }

        public Album MergeDataModel(Album model)
        {
            model.Title = Title;
            model.Url = Url;
            model.Detail = Detail;
            model.Thumbnail = Thumbnail;
            return model;
        }
        #endregion
    }

}
