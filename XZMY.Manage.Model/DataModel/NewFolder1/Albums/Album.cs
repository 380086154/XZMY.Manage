using System;
using System.Runtime.Serialization;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.Albums
{
    /// <summary> 
    /// 实体类
    /// </summary> 
    [Serializable]
    [DataContract]
    public class Album : EntityBase, IDataModel
    {
        #region Properties 

        /// <summary>111
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("")] 
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String Detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Url")] 
        //[DisplayName("")] 
        [DataMember]
        public String Url { get; set; }
        [DataMember]
        public String Thumbnail { get; set; }

        #endregion

        #region Collection

        #endregion
    }
}